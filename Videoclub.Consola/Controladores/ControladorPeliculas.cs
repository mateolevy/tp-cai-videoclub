using Videoclub.Entidades;
using Videoclub.Negocio;

namespace Videoclub.Consola.Controladores;

internal class ControladorPeliculas
{
    internal static void ConsultarTodasLasPeliculas()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Todas las Películas\n");

        try
        {
            var peliculaNegocio = new PeliculaNegocio();   

            // Traemos Peliculas e imprimimos.
            var peliculasResponse = peliculaNegocio.ConsultarPeliculas();
        
            // Verificamos si existen Peliculas.
            if (peliculasResponse.Data.Any())
            {
                PrintTablaPeliculas(peliculasResponse.Data);
            }
            else
            {
                Utilidades.MensajeError("No existen películas registradas.");
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al consultar todas las peliculas. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
        }
        finally
        {
            Console.WriteLine( "\nPresione una tecla para continuar");
            Console.ReadKey();
        }
    }
    
    internal static void ConsultarPeliculaPorId()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Película\n");

        try
        {
            var peliuclaNegocio = new PeliculaNegocio();

            if (peliuclaNegocio.ConsultarPeliculas().Data.Any())
            {
                // Pedimos id de pelicula a buscar.
                var idPelicula = Utilidades.PedirInt("Ingrese el ID de la película que desea visualizar:");

                var peliculaResponse = peliuclaNegocio.ConsultarPeliculaPorId(idPelicula);

                if (peliculaResponse.Success)
                {
                    PrintPelicula(peliculaResponse.Data);
                }
                else
                {
                    Utilidades.MensajeError("No existe una película registrada bajo el ID ingresado.");
                }
            }
            else
            {
                Utilidades.MensajeError("No existen películas registradas");
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError(
                $"\nError al consultar película existente. Descripción del Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine( "\nPresione una tecla para continuar");
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
            Console.WriteLine("Pantalla de Ingreso de Películas.\n");

            int anio = Utilidades.PedirInt("Ingrese el Año de Estreno:");
            string titulo = Utilidades.PedirString("Ingrese el Título:").ToUpper();
            string productora = Utilidades.PedirString("Ingrese la Productora:").ToUpper();
            string genero = Utilidades.PedirString("Ingrese el Género:").ToUpper();
            string director = Utilidades.PedirString("Ingrese el Director:");
            int duracion = Utilidades.PedirInt("Ingrese la duración en minutos:");

            // Validamos cliente previo a su registro
            Console.Clear();
            Console.WriteLine("\nSe han ingresado los siguientes datos de película: " +
                              $"\nAño: {anio}" +
                              $"\nTítulo: {titulo}" +
                              $"\nProductora: {productora}" +
                              $"\nGénero: {genero}" +
                              $"\nDirector: {director}" +
                              $"\nDuración: {duracion}");
            int opcMenu = Utilidades.PedirMenu("1. Continuar. \n2. Abortar.", 1, 2);
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
                        Utilidades.MensajeExito("Película agregada con éxito.");
                        Console.ReadKey();
                    }

                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de película abortado.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError(
                $"\nError al agregar película. \nDescripción del Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine( "\nPresione una tecla para continuar");
            Console.ReadKey();
        }
    }
    
    private static void PrintPelicula(Pelicula pelicula)
    {
        PrintTablaPeliculas(new List<Pelicula> { pelicula });
    }
    
    private static void PrintTablaPeliculas(List<Pelicula> peliculas)
    {
        // Header de la tabla
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}", "Título", "Género", "Año", "Productora", "Director", "Duración");
        Console.ForegroundColor = ConsoleColor.White;

        foreach (var pelicula in peliculas)
        {
            Console.WriteLine(
                "{0, -15} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}", pelicula.Titulo, pelicula.Genero, pelicula.Anio, pelicula.Productora, pelicula.Director, pelicula.Duracion + " minutos");
        }
    }
}