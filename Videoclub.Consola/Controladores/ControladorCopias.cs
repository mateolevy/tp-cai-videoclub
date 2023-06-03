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
        throw new NotImplementedException();
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