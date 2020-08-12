using System;
using System.IO;

namespace Program.ClasesAuxiliares
{
    class Peliculas
    {
		//Crear un string con la direccion de los archivos donde se guarda la informacion de las peliculas y
		//las peliculas con el nombre del usuario que la arquilo
        private const string ArchivoPeliculas = "Archivos/Peliculas.txt";
		private const string ArchivoAlquilo = "Archivos/alquilado.txt";

		//Aqui es donde se registran las peliculas
        public void registrarPeliculas(string nombre, string costo, DateTime time)
        {
			// este verifica que si el archvio Peliculas.txt existe
            if(File.Exists(ArchivoPeliculas))
            {
		        if(File.ReadAllText(ArchivoPeliculas).Equals(""))
		        {
			        File.AppendAllText(ArchivoPeliculas, nombre + "." + costo + "." + time.ToShortDateString());
		        }
		        else
		        {
					File.AppendAllText(ArchivoPeliculas,"\n" + nombre + "." + costo + "." + time.ToShortDateString());
		        }
	        }
	        else
	        {
				File.AppendAllText(ArchivoPeliculas, nombre + "." + costo + "." + time.ToShortDateString());
			}
    	}
		
		//aqui es donde se pueden alquilar las peliculas
		public void alquilarPelicula(string vnombre, string peliculaAlquilada)
		{
			Console.WriteLine(File.ReadAllText(ArchivoPeliculas));

			if(File.Exists(ArchivoAlquilo))
            {
		        if(File.ReadAllText(ArchivoAlquilo).Equals(""))
		        {
			        File.AppendAllText(ArchivoAlquilo, vnombre + "." + peliculaAlquilada);
		        }
		        else
		        {
					File.AppendAllText(ArchivoAlquilo,"\n" + vnombre + "." + peliculaAlquilada);
		        }
	        }
	        else
	        {
				File.AppendAllText(ArchivoAlquilo, vnombre + "." + peliculaAlquilada);
			}
		}

		//aqui se cambia la informacion de una pelicula
		public void editarPeliculas(string nombre, string costo, DateTime time, string nombreDeLaPelicula)
		{
			string[] datos = File.ReadAllLines(ArchivoPeliculas);
            string resultado = "";
            
            for(int i = 0; i < datos.Length; i++)
            {
                if(datos[i].Split('.')[0] != nombreDeLaPelicula)
                resultado += datos[i]+"\n";
            }
            resultado += nombre + "." + costo + "." + time.ToShortDateString();

            File.WriteAllText(ArchivoPeliculas, resultado);
		}

		//aqui es donde se eliminar una pelicula
		public void eliminarPelicula(string opcion)
		{
            int sumaTotal = 0;

            string[] datos = File.ReadAllLines(ArchivoPeliculas);
            string datosFinales = "";

            for(int i = 0; i < datos.Length; i++)
            {
                if(datos[i].Split('.')[0] != opcion){

                    if(sumaTotal != datos.Length-2)
                        datosFinales += datos[i]+"\n";
                    else
                        datosFinales += datos[i];
                        sumaTotal++; 
                }
            }
            File.WriteAllText(ArchivoPeliculas, datosFinales);
		}

		//aqui se puede ver las ganancias que se obtuvieron con las peliculas alquiladas
		public decimal ganadoConPeliculas()
		{
			string[] datos = File.ReadAllLines(ArchivoPeliculas);
			string[] datos1 = File.ReadAllLines(ArchivoAlquilo);
			decimal sumaTotal = 0;

			for(int j = 0; j < datos.Length; j++)
			{
				for(int i = 0; i < datos1.Length; i++)
				{
					if(datos[j].Split('.')[0] == datos1[i].Split('.')[1])
					{
						sumaTotal += decimal.Parse(datos[j].Split('.')[1]);
					}
				}
			}
			return sumaTotal;
		}

		//aqui se puede ver todas la peliculas que se alquilaron
		public void peliculasAlquiladas()
		{
			string[] datos = File.ReadAllLines(ArchivoAlquilo);

			for(int i = 0; i < datos.Length; i++)
			{
				Console.WriteLine(datos[i].Split('.')[1]);
			}
			Console.ReadKey();
		}
		public void peliculasAEntregar()
		{

		//aqui es donde se verifica el nombre de una nueva pelicula registrar para no crear peliculas con el mismo nombre
		}
		public bool existePelicula(string nombre)
        {
            string[] datos = File.ReadAllLines(ArchivoPeliculas);

            //este verifica si el nombre que de la pelicula que intenta registrar o introducir un nuevo nombre existe
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