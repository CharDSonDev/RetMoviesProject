using System;
using Program.ClasesAuxiliares;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Menus menus = new Menus();

            string login = "";
            int opcion = 0;

            do    
            {
                Console.Clear();
                Console.WriteLine("Bienvenido\n");
                Console.WriteLine("1.- Iniciar session \n2.- Registrase \n3.- Salir");
                login = Console.ReadLine();
                switch (login)
                {
                    case "1":
                    menus.opcionLogin();
                    break;
                    case "2":
                    menus.opcionRegistrarse();
                    break;
                    case "3":
                    Console.Clear();
                    Environment.Exit(1);
                    break;
                }
            }while (opcion != 4);
        }
    }
}