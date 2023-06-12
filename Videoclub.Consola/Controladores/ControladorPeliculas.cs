using Videoclub.Entidades;
using Videoclub.Negocio;

namespace Videoclub.Consola.Controladores;

internal class ControladorPeliculas
{
    internal static void ConsultarTodasLasPeliculas()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Todas las Peliculas\n");

        try
        {
            var peliculaNegocio = new PeliculaNegocio();   

            // Traemos Peliculas e imprimimos.
            var peliculasResponse = peliculaNegocio.ConsultarPeliculas();
        
            // Verificamos si existen Peliculas.
            if (peliculasResponse.Data.Any())
            {
                // Header de la tabla
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}", "Título", "Género", "Año", "Productora", "Director", "Duración\n");
                Console.ForegroundColor = ConsoleColor.Gray;

                foreach (var pelicula in peliculasResponse.Data)
                {
                    Console.WriteLine(
                   "{0, -15} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}", pelicula.Titulo, pelicula.Genero, pelicula.Anio, pelicula.Productora, pelicula.Director, pelicula.Duracion + "minutos");
                }
            }
            else
            {
                Utilidades.MensajeError("No existen peliculas registradas.");
            }

            Console.WriteLine("\nPresione una tecla para continuar.");
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al consultar todas las peliculas. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
        }
        finally
        {
            Console.ReadKey();
        }
    }
    
    internal static void ConsultarPeliculaPorId()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Pelicula\n");

        try
        {
            var peliuclaNegocio = new PeliculaNegocio();

            if (peliuclaNegocio.ConsultarPeliculas().Data.Any())
            {
                // Pedimos id de pelicula a buscar.
                var idPelicula = Utilidades.PedirInt("Ingrese el ID de la pelicula que desea visualizar:");

                var peliculaResponse = peliuclaNegocio.ConsultarPeliculaPorId(idPelicula);

                if (peliculaResponse.Success)
                {
                    // Header de la tabla
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}", "Título", "Género", "Año", "Productora", "Director", "Duración\n");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.WriteLine(
                        "{0, -15} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}", peliculaResponse.Data.Titulo,
                        peliculaResponse.Data.Genero, peliculaResponse.Data.Anio, peliculaResponse.Data.Productora,
                        peliculaResponse.Data.Director, peliculaResponse.Data.Duracion + "minutos");
                }
                else
                {
                    Utilidades.MensajeError("No existe una pelicula registrada bajo el ID ingresado.");
                }

                Console.WriteLine("\nPresione una tecla para continuar.");
            }
            else
            {
                Utilidades.MensajeError("No existen peliculas registradas");
                Console.WriteLine( "\nPresione una tecla para continuar");
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError(
                $"\nError al consultar pelicula existente. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
        }
        finally
        {
            Console.ReadKey();
        }
    }

    internal static void IngresarNuevaPelicula()
    {
        Console.Clear();

        try
        {
            var peliculaNegocio = new PeliculaNegocio();

            // Datos de entrada para nueva pelicula.
            Console.WriteLine("Pantalla de Ingreso de Peliculas.\n");

            int anio = Utilidades.PedirInt("Ingrese el Año de estreno:");
            string titulo = Utilidades.PedirString("Ingrese el Titulo:").ToUpper();
            string productora = Utilidades.PedirString("Ingrese la Productora:").ToUpper();
            string genero = Utilidades.PedirString("Ingrese el Genero:").ToUpper();
            string director = Utilidades.PedirString("Ingrese el Director:");
            int duracion = Utilidades.PedirInt("Ingrese la duracion en minutos:");

            // Validamos cliente previo a su registro
            Console.Clear();
            Console.WriteLine("\nSe han ingresado los siguientes datos de pelicula: " +
                $"\nAño: {anio}" +
                $"\nTitulo: {titulo}" +
                $"\nProductora: {productora}" +
                $"\nGenero: {genero}" +
                $"\nDirector: {director}" +
                $"\nDuracion: {duracion}");
            int opcMenu = Utilidades.PedirMenu("\n1. Continuar 2. Abortar", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nueva pelicula.
                    Pelicula nuevaPelicula = new Pelicula(0, anio, titulo, productora, genero, director, duracion);

                    // Agregamos nuevo cliente y traemos la info sobre como salio la operacion con un booleano.
                    var nuevaPeliculaOk = peliculaNegocio.AltaPelicula(nuevaPelicula);
                    if (nuevaPeliculaOk)
                    {
                        Console.Clear();
                        Utilidades.MensajeExito("Pelicula agregado con éxito! \nPresione una tecla para continuar.");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de pelicula abortado! \nPresione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }            
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al agregar pelicula. \nDescripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
}