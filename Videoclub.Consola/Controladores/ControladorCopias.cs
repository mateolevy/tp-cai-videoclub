using System.Globalization;
using Videoclub.Entidades;
using Videoclub.Negocio;

namespace Videoclub.Consola.Controladores;

internal class ControladorCopias
{
    internal static void ConsultarTodasLasCopias()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Todas las Copias\n");

        try
        {
            var copiasNegocio = new CopiaNegocio();
            var peliculasNegocio = new PeliculaNegocio();

            // Traemos copias (y peliculas para imprimir el nombre)
            var copiasResponse = copiasNegocio.ConsultarCopias();
            var peliculasResponse = peliculasNegocio.ConsultarPeliculas();

            // Verificamos que haya copias e imprimimos
            if (copiasResponse.Data.Any() && peliculasResponse.Data.Any())
            {
                PrintTablaCopiasYPeliculas(copiasResponse.Data, peliculasResponse.Data);
            }
            else
            {
                Utilidades.MensajeError("No existen copias registradas.");
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError(
                $"\nError al consultar todas las copias. Descripción del Error: {ex.Message}.");
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
    internal static void VisualizarReporteCopiasPorPelicula()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Copias por Película\n");

        try
        {
            var peliculaDatos = new PeliculaNegocio();
            var copiasDatos = new CopiaNegocio();

            int idPelicula;

            // Verificamos que haya copias e imprimimos
            if (copiasDatos.ConsultarCopias().Data.Any() && peliculaDatos.ConsultarPeliculas().Data.Any())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Películas:\n");

                    var peliculasResponse = peliculaDatos.ConsultarPeliculas();

                    if (peliculasResponse.Success && peliculasResponse.Data.Any())
                    {
                        PrintTablaPeliculas(peliculasResponse.Data);
                    }
                    else
                    {
                        Utilidades.MensajeError("No existen películas registradas.");
                        break;
                    }

                    idPelicula = Utilidades.PedirInt("\nIngrese el Id de la Película:");

                    // Validamos el Id de Pelicula ingresado.
                    var peliculaResponse = peliculaDatos.ConsultarPeliculaPorId(idPelicula);
                    if (peliculaResponse.Success)
                    {
                        Console.Clear();
                        var pelicula = peliculaResponse.Data;
                        Utilidades.MensajeExito($"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.Id}");
                        int opc = Utilidades.PedirMenu("1. Continuar. \n2. Eligir Nueva Película.", 1, 2);
                        switch (opc)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine($"Consulta de Copias para la Película: {pelicula.Titulo}:\n");

                                var copiasDePeliculaElegida = copiasDatos.ConsultarCopiasPorIdPelicula(pelicula.Id);

                                if (copiasDePeliculaElegida.Success && copiasDePeliculaElegida.Data.Any())
                                {
                                    var copias = copiasDePeliculaElegida.Data;
                                    PrintTablaCopias(copias);
                                }
                                else
                                {
                                    Utilidades.MensajeError($"No existen copias registradas para la película: {pelicula.Titulo}.");
                                }
                                break;
                            case 2:
                                break;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError($"No se encontró el Id de película: {idPelicula}.");
                    }

                    break;
                }
            }
            else
            {
                Utilidades.MensajeError("No existen copias registradas para ninguna película.");
            }
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar copias. \nDescripción del Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
    internal static void IngresarNuevaCopia()
    {
        Console.Clear();

        try
        {
            var copiaNegocio = new CopiaNegocio();

            int idPelicula = 0;
            string nombrePelicula = "";
            bool volverAlMenuPrincipal = false;
                       
            var peliculaNegocio = new PeliculaNegocio();

            // Traemos datos de las películas.
            var peliculasResponse = peliculaNegocio.ConsultarPeliculas();

            Console.WriteLine("Pantalla de Ingreso de Copias");

            // Mostramos pelculas disponibles y pedimos al usuario que ingrese el Id de la misma.
            if (volverAlMenuPrincipal == false)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Películas Disponibles:\n");

                    if (peliculasResponse.Success && peliculasResponse.Data.Any())
                    {
                        PrintTablaPeliculas(peliculasResponse.Data);
                        idPelicula = Utilidades.PedirInt("\nIngrese el Id de la Película que desea hacer una copia:");
                    }
                    else
                    {
                        Utilidades.MensajeError("No existen películas registradas.");
                        volverAlMenuPrincipal = true;
                        break;
                    }
                    
                    var peliculaPorId = peliculaNegocio.ConsultarPeliculaPorId(idPelicula);
                    
                    // Validamos el Id de Pelicula ingresado.
                    if (peliculaPorId.Success)
                    {
                        Console.Clear();
                        var pelicula = peliculaPorId.Data;
                        nombrePelicula = pelicula.Titulo;
                        Utilidades.MensajeExito($"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.Id}");
                        int opc = Utilidades.PedirMenu("1. Continuar. \n2. Eligir Nueva Película.", 1, 2);
                        switch (opc)
                        {
                            case 1: break;
                            case 2: continue;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError("No se encontró el Id de película ingresada.");
                    }
                    break;
                }
            }

            if (volverAlMenuPrincipal) 
                return;
            
            Console.Clear();

            //Datos de entrada para la nueva copia
            DateTime fechaAltaCopia = DateTime.Now;
            decimal precioCopia = Utilidades.PedirInt("Ingrese el Precio:");
            string observaciones = Utilidades.PedirString("Ingrese Observaciones:");

            // Validamos prestamo previo a su ingreso
            Console.Clear();
            Console.WriteLine("\nSe han ingresado los siguientes datos de copia:" +
                              $"\nPelícula: {nombrePelicula} con Id: {idPelicula}" +
                              $"\nPrecio de la copia: {precioCopia}" +
                              $"\nObservaciones: {observaciones}");
                              
            int opcMenu = Utilidades.PedirMenu("1. Continuar. 2. Abortar.", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nuevo prestamo
                    Copia nuevaCopia = new Copia(0, idPelicula, fechaAltaCopia, observaciones, precioCopia);

                    //Agregamos prestamo e informamos si se realiz correctamente o no 
                    var nuevaCopiaRes = copiaNegocio.AltaCopia(nuevaCopia);
                    if (nuevaCopiaRes)
                    {
                        Console.Clear();
                        Utilidades.MensajeExito("\nCopia agregada con éxito.");
                    }
                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de copia abortado.");
                    break;
            }

        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al agregar copia. Descripción del Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    private static void PrintTablaCopiasYPeliculas(List<Copia> copias, List<Pelicula> peliculas)
    {
        string precio = "No Registrado";

        // Header de la tabla
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -8} | {1, -15} | {2, -15} | {3, -20} | {4, -15} | {5, -30}", "Id Copia", "Id Película", "Titulo Película", "Fecha Alta", "Precio", "Observaciones");
        Console.ForegroundColor = ConsoleColor.White;

        foreach (var copia in copias)
        {
            foreach (var pelicula in peliculas)
            {
                if (copia.Id == pelicula.Id)
                    if (!string.IsNullOrEmpty(copia.Precio.ToString(CultureInfo.InvariantCulture)))
                    {
                        precio = "$ " + copia.Precio;
                    }

                Console.WriteLine("{0, -8} | {1, -15} | {2, -15} | {3, -20} | {4, -15} | {5, -30}", copia.Id, pelicula.Id, pelicula.Titulo,
                    copia.FechaAlta, precio, copia.Observaciones);
            }
        }
    }
    
    private static void PrintTablaCopias(List<Copia> copias)
    {
        // Header de la tabla
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -20} | {2, -15} | {3, -30}", "Id Copia", "Fecha Alta", "Precio", "Observaciones");
        Console.ForegroundColor = ConsoleColor.White;

        string precio = "No Registrado";
        foreach (var copia in copias)
        {
            if (!string.IsNullOrEmpty(copia.Precio.ToString(CultureInfo.InvariantCulture)))
            {
                precio = "$ " + copia.Precio;
            }
            Console.WriteLine("{0, -15} | {1, -20} | {2, -15} | {3, -30}", copia.Id, copia.FechaAlta, precio, copia.Observaciones);
        }
    }
    
    private static void PrintTablaPeliculas(List<Pelicula> peliculas)
    {
        // Header de la tabla
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -15}", "Id Película", "Título");
        Console.ForegroundColor = ConsoleColor.White;

        foreach (var pelicula in peliculas)
        {
            Console.WriteLine("{0, -15} | {1, -15}", pelicula.Id, pelicula.Titulo);
        }
    }
}