using System;
using System.IO;

namespace Program.ClasesAuxiliares
{
    class Menus
    {
        private Usuarios usuarios;
        private Peliculas peliculas;
        public Menus()
        {
            usuarios = new Usuarios();
            peliculas = new Peliculas();
        }
        
        string client, admin, adminUsers, adminPeliculas;
        int opcion = 0;
        int opcion1 = 0;
        int opcion2 = 0;
        // este menu es donde los usuarios iniciaran session
        public void opcionLogin()
        {
            string nombre, password, tipoUser;

            Console.Clear();
            Console.Write("Ingrese usuario: ");
            nombre = Console.ReadLine();
            Console.Write("Ingrese contraseña: ");
            password = Console.ReadLine();
            Console.Write("Ingrese el tipo de usuario ej. cliente/administrador: ");
            tipoUser = Console.ReadLine();
            bool valor = usuarios.loginUsuario(nombre, password, tipoUser);

            if (valor && tipoUser.ToLower() == "administrador")
            {
                opcionLoginAdministrador(nombre, password, tipoUser);
            }
            else if (valor && tipoUser.ToLower() == "cliente")
            {
                opcionLoginCliente(nombre, password, tipoUser);
            }
            else
            {
                Console.WriteLine("\nUsuario no registrado");
                Console.ReadKey();
            }
        }
        // este menu es donde el usurio se registrara con su nombre, contraseña, tipo de usuario y su direccion
        public void opcionRegistrarse()
        {
            string nombre, password, tipoUser, direccionDelUsuario;
            Console.Clear();
            Console.Write("Ingrese nombre para el usuario: ");
            nombre = Console.ReadLine();
            Console.Write("Ingrese contraseña para el usuario: ");
            password = Console.ReadLine();
            Console.Write("ingrese el tipo de usuario ej. cliente/administrador: ");
            tipoUser = Console.ReadLine();
            Console.Write("Ingrese su direccion: ");
            direccionDelUsuario = Console.ReadLine();
            bool resul = usuarios.existeUsuario(nombre);
            if(resul)
            {
                Console.WriteLine("\nYa existe un usuario con este nombre");
                Console.ReadKey();
            }
            else
            {
                usuarios.registrarUsuario(nombre, password, tipoUser, direccionDelUsuario);
            }
        }
        // este menu es el menu del usuario administrador
        public void opcionLoginAdministrador(string nombre, string password, string tipoUser)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Bienvenido " + nombre + "\n\n1.- Editar mi usuario \n2.- Registrar pelicula \n3.- Administrar peliculas \n4.- Administrar usuarios \n5.- Alquilar pelicula \n6.- Ver ganancias \n7.- Ver peliculas \n8.- Salir");
                admin = Console.ReadLine();
                switch (admin)
                {
                    //aqui es el administrador puede editar su pefil
                    case "1":
                        Console.Clear();
                        Console.Write("Nuevo nombre: ");
                        string newNombre = Console.ReadLine();
                        Console.Write("Nueva contraseña: ");
                        string newPassword = Console.ReadLine();
                        Console.Write("Nuevo tipo de usuario: ");
                        string newTipoUser = Console.ReadLine();
                        Console.Write("Nueva direccion: ");
                        string newDireccionDelUsuario = Console.ReadLine();
                        bool resul = usuarios.existeUsuario(newNombre);
                        if(resul)
                        {
                            Console.WriteLine("\nYa existe un usuario con este nombre");
                            Console.ReadKey();
                        }
                        else
                        {
                            usuarios.editarUsuario(newNombre, newPassword, newTipoUser, newDireccionDelUsuario, nombre);
                        }
                    break;
                    case "2":
                    //aqui es donde el administrador registra un pelicula
                        Console.Clear();
                        Console.Write("Ingrese nombre para la pelicula: ");
                        string nameMov = Console.ReadLine();
                        Console.Write("Ingrese costo para la pelicula: ");
                        string costo = Console.ReadLine();
                        Console.Write("Ingrese la fecha de vencimiento ej. dia,mes,año: ");
                        DateTime time = DateTime.Parse(Console.ReadLine());
                        bool resul2 = peliculas.existePelicula(nameMov);
                        if(resul2)
                        {
                            Console.WriteLine("\nYa existe una pelicula con este nombre");
                            Console.ReadKey();
                        }
                        else
                        {
                            peliculas.registrarPeliculas(nameMov, costo, time);
                        }
                    break;
                    case "3":
                    do
                    {
                        //este es el menu donde el administrador administra las peliculas registradas
                        Console.Clear();
                        Console.WriteLine("1.- Editar Peliculas \n2.- Eliminar Pelicula");
                        adminPeliculas = Console.ReadLine();
                        switch (adminPeliculas)
                        {
                            //aqui el administror edita la informacion de las peliculas
                            case "1":
                            Console.Clear();
                            int numero = 0;
                            string[] datos = File.ReadAllLines("Archivos/Peliculas.txt");

                            for(int i = 0; i < datos.Length; i++)
                            {
                                Console.WriteLine(i+1 + ". " + datos[i]);
                            }
                            Console.Write("Elija la pelicula: ");
                            numero = int.Parse(Console.ReadLine());
                            Console.Write("Nuevo nombre: ");
                            string newNombrePeli = Console.ReadLine();
                            Console.Write("Nueva costo: ");
                            string newCosto = Console.ReadLine();
                            Console.Write("Nuevo tiempo de vencimiento ej. dia,mes,año: ");
                            DateTime newTime = DateTime.Parse(Console.ReadLine());
                            bool resul3 = peliculas.existePelicula(newNombrePeli);
                            if(resul3)
                            {
                                Console.WriteLine("\nYa existe una pelicula con este nombre");
                                Console.ReadKey();
                            }
                            else
                            {
                                peliculas.editarPeliculas(newNombrePeli, newCosto, newTime, datos[numero-1].Split('.')[0]);
                            }
                            break;
                            case "2":
                            //aqui el administrador elimina peliculas
                            Console.Clear();
                            int pocision2 = 0;
                            string[] info1 = File.ReadAllLines("Archivos/Peliculas.txt");
                            for(int i =0; i < info1.Length; i++)
                            {
                                Console.WriteLine(i+1 + ". " + info1[i]);
                            }
                            Console.Write("Elija un usuario: ");
                            pocision2 = int.Parse(Console.ReadLine());
                            peliculas.eliminarPelicula(info1[pocision2-1].Split('.')[0]);
                            break;
                        }
                    }while (opcion2 != 3);
                    break;
                    case "4":
                        do
                        {
                            //en este menu el administrador administra los usuarios registrados
                            Console.Clear();
                            Console.WriteLine("1.- Editar Usuarios \n2.- Eliminar usuario");
                            adminUsers = Console.ReadLine();
                            switch (adminUsers)
                            {
                                //aqui el administrador edita la informacion de los usurios
                                case "1":
                                Console.Clear();
                                int pocision2 = 0;
                                string[] info2 = File.ReadAllLines("Archivos/Usuarios.txt");
                                for(int i =0; i < info2.Length; i++)
                                {
                                    Console.WriteLine(i+1 + ". " + info2[i]);
                                }
                                Console.Write("Elija un usuario: ");
                                pocision2 = int.Parse(Console.ReadLine());
                                Console.Write("Nuevo nombre: ");
                                newNombre = Console.ReadLine();
                                Console.Write("Nueva contraseña: ");
                                newPassword = Console.ReadLine();
                                Console.Write("Nuevo tipo de usuario: ");
                                newTipoUser = Console.ReadLine();
                                Console.Write("Nueva direccion: ");
                                newDireccionDelUsuario = Console.ReadLine();
                                bool resul1 = usuarios.existeUsuario(newNombre);
                                if(resul1)
                                {
                                    Console.WriteLine("\nYa existe un usuario con este nombre");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    usuarios.editarUsuario(newNombre, newPassword, newTipoUser, newDireccionDelUsuario, info2[pocision2-1].Split('.')[0]);
                                }
                                break;
                                case "2":
                                //aqui el administrador eliminar usuarios
                                Console.Clear();
                                int pocision1 = 0;
                                string[] info1 = File.ReadAllLines("Archivos/Usuarios.txt");
                                for(int i =0; i < info1.Length; i++)
                                {
                                    Console.WriteLine(i+1 + ". " + info1[i]);
                                }
                                Console.Write("Elija un usuario: ");
                                pocision1 = int.Parse(Console.ReadLine());
                                usuarios.eliminarUsuario(info1[pocision1-1].Split('.')[0]);
                                break;
                            }
                        }while (opcion1 != 3);
                    break;
                    case "5":
                    //aqui es donde el administrador puede alquilar una pelicula
                    Console.Clear();
                    int numero4 = 0;
                    string[] datos4 = File.ReadAllLines("Archivos/Peliculas.txt");

                    for(int i = 0; i < datos4.Length; i++)
                    {
                        Console.WriteLine(i+1 + ". " + datos4[i]);
                    }
                    Console.Write("Elija la pelicula: ");
                    numero4 = int.Parse(Console.ReadLine());
                    peliculas.alquilarPelicula(nombre, datos4[numero4-1].Split('.')[0]);
                    break;
                    case "6":
                    //en esta parte se puede ver el dinero ganado por las peliculas que fueron alquiladas
                    Console.Clear();
                    decimal sumaTotal = peliculas.ganadoConPeliculas();
                    Console.WriteLine("\nGanancia total: " + sumaTotal);
			        Console.ReadKey();
                    break;
                    case "7":
                    //aqui se puede ver las peliculas alquiladas
                    Console.Clear();
                    peliculas.peliculasAlquiladas();
                    break;
                    case "8":
                    //aqui es donde esta el opcion salir
                    Console.Clear();
                    Environment.Exit(1);
                    break;
                }
            } while (opcion != 9);
        }
        public void opcionLoginCliente(string nombre, string password, string tipoUser)
        {
            do
            {
                //este el menu de opciones para el cliente
                Console.Clear();
                Console.WriteLine("Bienvenido " + nombre + "\n\n1.- Editar usuario \n2.- Alquilar una pelicula \n3.- Ver peliculas alquiladas \n4.- Salir");
                client = Console.ReadLine();
                switch (client)
                {
                    case "1":
                    //en este menu el cliente puede editar su informacion
                        Console.Clear();
                        Console.Write("Nuevo nombre: ");
                        string newNombre = Console.ReadLine();
                        Console.Write("Nueva contraseña: ");
                        string newPassword = Console.ReadLine();
                        Console.Write("Nuevo tipo de usuario: ");
                        string newTipoUser = Console.ReadLine();
                        Console.Write("Nueva direccion: ");
                        string newDireccionDelUsuario = Console.ReadLine();
                        bool resul = usuarios.existeUsuario(newNombre);
                        if(resul)
                        {
                            Console.WriteLine("\nYa existe un usuario con este nombre");
                            Console.ReadKey();
                        }
                        else
                        {
                            usuarios.editarUsuario(newNombre, newPassword, newTipoUser, newDireccionDelUsuario, nombre);
                        }
                    break;
                    case "2":
                    //aqui el cliente puede alquilar una de las peliculas registradas
                        Console.Clear();
                        int numero = 0;
                        string[] datos = File.ReadAllLines("Archivos/Peliculas.txt");

                        for(int i = 0; i < datos.Length; i++)
                        {
                            Console.WriteLine(i+1 + ". " + datos[i]);
                        }
                        Console.Write("Elija la pelicula: ");
                        numero = int.Parse(Console.ReadLine());
                        peliculas.alquilarPelicula(nombre, datos[numero-1].Split('.')[0]);
                    break;
                    case "3":
                    //aqui es donde el cliente puede ver la peliculas alquiladas
                        Console.Clear();
                        peliculas.peliculasAlquiladas();
                    break;
                    case "4":
                    //aqui es donde esta el opcion salir
                    Console.Clear();
                    Environment.Exit(1);
                    break;
                }
            } while (opcion != 5);
        }
    }
}