using Videoclub.Negocio;

namespace Videoclub.Consola.Controladores;

internal class ControladorCopias
{
    internal static void ConsultarTodasLasCopias()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de consulta de todas las Copias\n");

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
                foreach(var copia in copiasResponse.Data)
                {
                    foreach(var pelicula in peliculasResponse.Data)
                    {
                        if(copia.Id == pelicula.Id)
                        Console.WriteLine($"Copia de la pelicula: {pelicula.Titulo}, realizada el dia {copia.FechaAlta}, Precio: ${copia.Precio} ID: {copia.Id}");
                    }                   
                }
                Console.WriteLine("\nPresione una tecla para continuar.");
                Console.ReadKey();
            }
            else
            {
                Utilidades.MensajeError("No existen copias registradas. \n Presione una tecla para continuar");
            }


        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al consultar todas las copias. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
        }
    }
    internal static void ConsultarCopiasPorIdPelicula()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Copias por Película\n");

        try
        {
            var peliculaDatos = new PeliculaNegocio();
            var copiasDatos = new CopiaNegocio();

            int idPelicula;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Películas:\n");
                
                var peliculasResponse = peliculaDatos.ConsultarPeliculas();
                foreach (var pelicula in peliculasResponse.Data)
                {
                    Console.WriteLine($"Id Película: {pelicula.Id} - Título: {pelicula.Titulo}");
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la Película:");

                // Validamos el Id de Pelicula ingresado.
                var peliculaResponse = peliculaDatos.ConsultarPeliculaPorId(idPelicula);
                if (peliculaResponse.Success)
                {
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
                                    foreach (var copia in copias)
                                    {
                                        Console.WriteLine(
                                            $"Id Copia: {copia.Id} - Fecha Alta Copia: {copia.FechaAlta} - Precio: ${copia.Precio} Observaciones: {copia.Observaciones}");
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
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar copias. \nDescripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
    internal static void IngresarNuevaCopia()
    {
        throw new NotImplementedException();
    }
    internal static void VisualizarReporteCopiasPorPelicula()
    {
        throw new NotImplementedException();
    }
}