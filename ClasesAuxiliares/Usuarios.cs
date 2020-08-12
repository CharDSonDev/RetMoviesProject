using System;
using System.IO;

namespace Program.ClasesAuxiliares
{
    class Usuarios
    {
        private const string ArchivoUsuarios = "Archivos/Usuarios.txt";
		private const string ArchivoAlquilo = "Archivos/alquilado.txt";

        public void registrarUsuario(string nombre, string password, string tipoUser, string direccionDelUsurio)
        {
            tipoUser = tipoUser.ToLower();
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
        
	    public bool loginUsuario(string nombre, string password, string tipoUser)
	    {
	        string[] nombres = new string[File.ReadAllLines(ArchivoUsuarios).Length];
	        string[] passwords = new string[nombres.Length];
            string[] tipoUsers = new string[nombres.Length];
            
            for(int i = 0; i < nombres.Length; i++)
            {
                nombres[i] = File.ReadAllLines(ArchivoUsuarios)[i].Split('.')[0];
                passwords[i] = File.ReadAllLines(ArchivoUsuarios)[i].Split('.')[1];
                tipoUsers[i] = File.ReadAllLines(ArchivoUsuarios)[i].Split('.')[2];
            }
            
            for(int i = 0; i < passwords.Length; i++)
            {
                if(nombres[i].Equals(nombre) && passwords[i].Equals(password) && tipoUsers[i].Equals(tipoUser.ToLower()))
                return true;
            }
            return false;
        }

        public void editarUsuario(string nombre, string password, string tipoUser, string direccionDelUsuario, string nombreDelUsuario)
        {
            string[] datos = File.ReadAllLines(ArchivoUsuarios);
            string[] datos1 = File.ReadAllLines(ArchivoAlquilo);
            string resultado = "";
            string resultado1 = "";
            string nombreDeSuPelicula = "";
            
            for(int i = 0; i < datos.Length; i++)
            {
                if(datos[i].Split('.')[0] != nombreDelUsuario)
                resultado += datos[i]+"\n";
            }
            for(int i = 0; i < datos1.Length; i++)
            {
                if(datos1[i].Split('.')[0] != nombreDelUsuario)
                resultado1 += datos1[i]+"\n";
            }
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

        public void eliminarUsuario(string opcion)
        {
            int contador = 0;

            string[] datos = File.ReadAllLines(ArchivoUsuarios);
            string datosFinales = "";

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

        public bool existeUsuario(string nombre)
        {
            string[] datos = File.ReadAllLines(ArchivoUsuarios);

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