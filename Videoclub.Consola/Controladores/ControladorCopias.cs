using Videoclub.Entidades;
using Videoclub.Negocio;
using ConsoleTables;
using System.Data;

namespace Videoclub.Consola.Controladores;

internal class ControladorCopias
{
    internal static void ConsultarTodasLasCopias()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de todas las Copias\n");

        try
        {
            var copiasNegocio = new CopiaNegocio();
            var peliculasNegocio = new PeliculaNegocio();

            // Traemos copias (y peliculas para imprimir el nombre)
            var copiasResponse = copiasNegocio.ConsultarCopias();
            var peliculasResponse = peliculasNegocio.ConsultarPeliculas();
            
            // Verificamos que haya copias e imprimimos
            if(copiasResponse.Data.Any() && peliculasResponse.Data.Any())
            {
                string precio = "No Registrado";

                // Header de la tabla
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -8}", "Título", "Fecha Alta", "Precio", "Id Copia\n");
                Console.ForegroundColor = ConsoleColor.Gray;

                foreach (var copia in copiasResponse.Data)
                {
                    foreach(var pelicula in peliculasResponse.Data)
                    {
                        if (copia.Id == pelicula.Id)
                            if (!string.IsNullOrEmpty(copia.Precio.ToString()))
                            {
                                precio = "$ " + copia.Precio;
                            }
                            Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -8} | {4, -2}", pelicula.Titulo, copia.FechaAlta, precio, copia.Id);
                    }                   
                }
                Console.WriteLine("\nPresione una tecla para continuar.");
                Console.ReadKey();
            }
            else
            {
                Utilidades.MensajeError("No existen copias registradas. \nPresione una tecla para continuar");
                Console.ReadKey();
            }


        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al consultar todas las copias. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
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

                    // Header de la tabla
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0, -15} | {1, -15}", "Id Película", "Título\n");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    foreach (var pelicula in peliculasResponse.Data)
                    {
                        Console.WriteLine("{0, -15} | {1, -15}", pelicula.Id, pelicula.Titulo);
                    }

                    idPelicula = Utilidades.PedirInt("\nIngrese el Id de la Película:");

                    // Validamos el Id de Pelicula ingresado.
                    var peliculaResponse = peliculaDatos.ConsultarPeliculaPorId(idPelicula);
                    if (peliculaResponse.Success)
                    {
                        Console.Clear();
                        var pelicula = peliculaResponse.Data;
                        Utilidades.MensajeExito($"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.Id}");
                        int opc = Utilidades.PedirMenu("1. Continuar \n2. Eligir nueva película", 1, 2);
                        switch (opc)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine($"Consulta de Copias para la Película: {pelicula.Titulo}:\n");

                                var copiasDePeliculaElegida = copiasDatos.ConsultarCopiasPorIdPelicula(pelicula.Id);

                                if (copiasDePeliculaElegida.Success)
                                {
                                    var copias = copiasDePeliculaElegida.Data;
                                    {
                                        // Header de la tabla
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30}", "Id Copia", "Fecha Alta Copia", "Precio", "Observaciones\n");
                                        Console.ForegroundColor = ConsoleColor.Gray;

                                        string precio = "No Registrado";
                                        foreach (var copia in copias)
                                        {
                                            if (!string.IsNullOrEmpty(copia.Precio.ToString()))
                                            {
                                                precio = "$ " + copia.Precio;
                                            }
                                            Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30}", copia.Id, copia.FechaAlta, precio, copia.Observaciones);
                                        }
                                        Console.WriteLine("\nPresione una tecla para continuar.");
                                        Console.ReadKey();
                                    }

                                }
                                break;
                            case 2:
                                break;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError($"No se encontró el Id de película: {idPelicula}. \nPresione una tecla para ingresar nueva película.");
                        Console.ReadKey();
                    }

                    break;
                }
            }
            else
            {
                Utilidades.MensajeError("No existen copias registradas. \nPresione una tecla para continuar");
                Console.ReadKey();
            }
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar copias. \nDescripción del Error: {ex.Message} \nPresione una tecla para continuar.");
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
            string nombrePelicula = null;
            bool volverAlMenuPrincipal = false;
                       
            var peliculaDatos = new PeliculaNegocio();

            // Traemos datos de las películas.
            var peliculasResponse = peliculaDatos.ConsultarPeliculas();

            Console.WriteLine("Pantalla de Ingreso de Copias");

            // Mostramos pelculas disponibles y pedimos al usuario que ingrese el Id de la misma.
            if (volverAlMenuPrincipal == false)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Películas Disponibles:\n");

                    // Header de la tabla
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0, -15} | {1, -15}", "Id Película", "Título\n");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    foreach (var pelicula in peliculasResponse.Data)
                    {
                        Console.WriteLine("{0, -15} | {1, -15}", pelicula.Id, pelicula.Titulo);
                    }
                    idPelicula = Utilidades.PedirInt("\nIngrese el Id de la Película que desea hacer una copia:");
                    var peliculaPorId = peliculaDatos.ConsultarPeliculaPorId(idPelicula);
                    // Validamos el Id de Pelicula ingresado.
                    if (peliculaPorId.Success)
                    {
                        Console.Clear();
                        var pelicula = peliculaPorId.Data;
                        nombrePelicula = pelicula.Titulo;
                        Utilidades.MensajeExito($"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.Id}");
                        int opc = Utilidades.PedirMenu("1. Continuar \n2. Eligir nueva pelicula", 1, 2);
                        switch (opc)
                        {
                            case 1: break;
                            case 2: continue;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError("No se encontró el Id de película ingresada. \nPresione una tecla para ingresar nueva película.");
                        Console.ReadKey();
                    }
                    break;
                }
            }


            if (volverAlMenuPrincipal) 
                return;
            
            Console.Clear();

            //Datos de entrada para la nueva copia
            DateTime fechaAltaCopia = DateTime.Now;
            decimal precioCopia = Utilidades.PedirInt("Ingrese el Precio ");;
            string observaciones = Utilidades.PedirString("Ingrese Observaciones:");

            // Validamos prestamo previo a su ingreso
            Console.Clear();
            Console.WriteLine("\nSe han ingresado los siguientes datos de copia:" +
                              $"\nPelícula: {nombrePelicula} con Id: {idPelicula}" +
                              $"\nPrecio de la copia: {precioCopia}" +
                              $"\nObservaciones: {observaciones}");
                              
            int opcMenu = Utilidades.PedirMenu("\n1. Continuar 2. Abortar", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nuevo prestamo
                    Copia nuevaCopia = new Copia(0, idPelicula, fechaAltaCopia, observaciones ?? string.Empty, precioCopia);

                    //Agregamos prestamo e informamos si se realiz correctamente o no 
                    var nuevaCopiaRes = copiaNegocio.AltaCopia(nuevaCopia);
                    if (nuevaCopiaRes)
                    {
                        Console.Clear();
                        Utilidades.MensajeExito("\nCopia agregada con exito! \nPresione una tecla para continuar.");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de Copia abortado! \nPresione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }

        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al agregar Copia. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
}