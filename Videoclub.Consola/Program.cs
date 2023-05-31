using Videoclub.Consola.Controladores;

namespace Videoclub.Consola;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        try
        {
            while (true)
            {
                Console.Clear();
                var opcMenu = Utilidades.PedirMenu("Bienvenido al 'Sistema de Gestión Video Club'!\n" +
                                                   "1. Menú Clientes \n2. Menú Préstamos \n3. Menú Películas \n4. Menú Copia de Películas \n5. Menú Reportes \n0. Salir", 0, 5);
                switch (opcMenu)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Gracias por utilizar nuestros servicios! \nPresione una tecla para salir del programa.");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.Clear();
                        var opcClientes = Utilidades.PedirMenu("Menú Clientes.\n1. Ingresar Nuevo Cliente \n2. Consultar Cliente Existente \n3. Consultar Todos los Clientes \n0. Volver al Menú Principal", 0, 3);
                        switch (opcClientes)
                        {
                            case 0: break;
                            case 1:
                                ControladorClientes.IngresarNuevoCliente();
                                continue;
                            case 2:
                                ControladorClientes.ConsultarClienteExistente();
                                continue;
                            case 3:
                                ControladorClientes.ConsultarTodosLosClientes();
                                continue;
                        }
                        continue;
                    case 2:
                        Console.Clear();
                        var opcPrestamos = Utilidades.PedirMenu("Menú Préstamos.\n1. Ingresar Préstamo \n2. Consultar Préstamo \n0. Volver al Menú Principal", 0, 2);
                        switch (opcPrestamos)
                        {
                            case 0: break;
                            case 1:
                                ControladorPrestamos.IngresarNuevoPrestamo();
                                continue;
                            case 2:
                                ControladorPrestamos.ConsultarPrestamoExistente();
                                continue;
                        }
                        continue;
                    case 3:
                        Console.Clear();
                        var opcPeliculas = Utilidades.PedirMenu("Menú Películas.\n1. Ingresar Película \n2. Consultar Película \n0. Volver al Menú Principal", 0, 2);
                        switch (opcPeliculas)
                        {
                            case 0: break;
                            case 1:
                                ControladorPeliculas.IngresarNuevaPelicula();
                                continue;
                            case 2:
                                ControladorPeliculas.ConsultarPeliculaExistente();
                                continue;
                        }
                        continue;
                    case 4:
                        Console.Clear();
                        var opcCopias = Utilidades.PedirMenu("Menú Copias.\n1. Ingresar Copia de Película \n2. Consultar Copia de Película \n0. Volver al Menú Principal", 0, 2);
                        switch (opcCopias)
                        {
                            case 0: break;
                            case 1:
                                ControladorCopias.IngresarCopiaPelicula();
                                continue;
                            case 2:
                                ControladorCopias.ConsultarCopiaPeliculaExistente();
                                continue;
                        }
                        continue;
                    case 5:
                        Console.Clear();
                        var opcReportes = Utilidades.PedirMenu("Menú Reportes.\n1. Visualizar Préstamos por CLiente \n2. Visualizar Copias por Película \n0. Volver al Menú Principal", 0, 2);
                        switch (opcReportes)
                        {
                            case 0: break;
                            case 1:
                                ControladorPrestamos.VisualizarPrestamosCliente();
                                continue;
                            case 2:
                                ControladorCopias.VisualizarCopiasPelicula();
                                continue;
                        }
                        continue;
                }
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"Error. Descripción del Error: {ex.Message}.");
        }
    }
}