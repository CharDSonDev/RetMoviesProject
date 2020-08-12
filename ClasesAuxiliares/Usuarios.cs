using System;
using System.IO;

namespace Program.ClasesAuxiliares
{
    class Usuarios
    {
        //Aqui se crean los strings con la direccion del archivo donde se almacenan los usuarios
        private const string ArchivoUsuarios = "Archivos/Usuarios.txt";
		private const string ArchivoAlquilo = "Archivos/alquilado.txt";

        public void registrarUsuario(string nombre, string password, string tipoUser, string direccionDelUsurio)
        {
            //este pone en minuscula todo el texto
            tipoUser = tipoUser.ToLower();
            // este verifica que si el archvio Ususarios.txt existe
            if(File.Exists(ArchivoUsuarios))
            {
		        if(File.ReadAllText(ArchivoUsuarios).Equals(""))
		        {
			        File.AppendAllText(ArchivoUsuarios, nombre + "." + password + "." + tipoUser + "." + direccionDelUsurio);
		        }
		        else
		        {
			        File.AppendAllText(ArchivoUsuarios,"\n" + nombre + "." + password + "." + tipoUser + "." + direccionDelUsurio);
		        }
	        }
	        else
	        {
		        File.AppendAllText(ArchivoUsuarios, nombre + "." + password + "." + tipoUser + "." + direccionDelUsurio);
	        }
	    }
        
        //aqui es donde se inicia session
	    public bool loginUsuario(string nombre, string password, string tipoUser)
	    {
	        string[] nombres = new string[File.ReadAllLines(ArchivoUsuarios).Length];
	        string[] passwords = new string[nombres.Length];
            string[] tipoUsers = new string[nombres.Length];
            
            //este verifica los datos que el usuario ingreso esten registrados
            for(int i = 0; i < nombres.Length; i++)
            {
                nombres[i] = File.ReadAllLines(ArchivoUsuarios)[i].Split('.')[0];
                passwords[i] = File.ReadAllLines(ArchivoUsuarios)[i].Split('.')[1];
                tipoUsers[i] = File.ReadAllLines(ArchivoUsuarios)[i].Split('.')[2];
            }
            
            //
            for(int i = 0; i < passwords.Length; i++)
            {
                if(nombres[i].Equals(nombre) && passwords[i].Equals(password) && tipoUsers[i].Equals(tipoUser.ToLower()))
                return true;
            }
            return false;
        }

        //aqui es donde se edita puede editar tu perfil o el perfil de los demas usuarios
        public void editarUsuario(string nombre, string password, string tipoUser, string direccionDelUsuario, string nombreDelUsuario)
        {
            string[] datos = File.ReadAllLines(ArchivoUsuarios);
            string[] datos1 = File.ReadAllLines(ArchivoAlquilo);
            string resultado = "";
            string resultado1 = "";
            string nombreDeSuPelicula = "";
            
            //este separa al usuario de los demas del archivo Usuarios
            for(int i = 0; i < datos.Length; i++)
            {
                if(datos[i].Split('.')[0] != nombreDelUsuario)
                resultado += datos[i]+"\n";
            }
            //este separa al usuario de los demas del archivo Alquilado
            for(int i = 0; i < datos1.Length; i++)
            {
                if(datos1[i].Split('.')[0] != nombreDelUsuario)
                resultado1 += datos1[i]+"\n";
            }
            //este seprar la pelicula de usuario con el nombre anterior para el nuevo nombre del usuario
            for(int i = 0; i < datos1.Length; i++)
            {
                if(datos1[i].Split('.')[0] == nombreDelUsuario)
                nombreDeSuPelicula += datos1[i].Split('.')[1];
            }
            resultado += nombre + "." + password + "." + tipoUser + "." + direccionDelUsuario;
            resultado1 += nombre + "." + nombreDeSuPelicula;

            File.WriteAllText(ArchivoUsuarios, resultado);
            File.WriteAllText(ArchivoAlquilo, resultado1);
        }

        //aqui es donde el administrador puede eliminar aun usuario
        public void eliminarUsuario(string opcion)
        {
            int contador = 0;

            string[] datos = File.ReadAllLines(ArchivoUsuarios);
            string datosFinales = "";

            //este verifica cual fue el usuario que selecciono eliminar
            for(int i = 0; i < datos.Length; i++)
            {
                if(datos[i].Split('.')[0] != opcion)
                {
                    if(contador != datos.Length-2)
                    datosFinales += datos[i]+"\n";
                    else
                    datosFinales += datos[i];
                    contador++; 
                }
            }
            File.WriteAllText(ArchivoUsuarios, datosFinales);
        }

        //aqui es donde se verifica el nombre de un nuevo usuario para crear usuarios con el mismo nombre
        public bool existeUsuario(string nombre)
        {
            string[] datos = File.ReadAllLines(ArchivoUsuarios);

            //este verifica si el nombre que del usuario que intenta registrar o introducir un nuevo nombre existe
            for(int i = 0; i < datos.Length; i++)
            {
                if(datos[i].Split('.')[0] == nombre)
                {
                    return true;
                }
            }
            return false;
        }
    }
}