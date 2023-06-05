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
                        Utilidades.MensajeExito("Gracias por utilizar nuestros servicios! \nPresione una tecla para salir del programa.");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.Clear();
                        var opcClientes = Utilidades.PedirMenu("Menú Clientes.\n1. Ingresar Nuevo Cliente \n2. Consultar Cliente por DNI \n3. Consultar Cliente por Telefono \n4. Consultar Todos los Clientes \n0. Volver al Menú Principal", 0, 4);
                        switch (opcClientes)
                        {
                            case 0: break;
                            case 1:
                                ControladorClientes.IngresarNuevoCliente();
                                continue;
                            case 2:
                                ControladorClientes.ConsultarClientePorDni();
                                continue;
                            case 3:
                                ControladorClientes.ConsultarClientePorTelefono();
                                continue;
                            case 4:
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
                                ControladorPrestamos.ConsultarPrestamosPorPelicula();
                                continue;
                        }
                        continue;
                    case 3:
                        Console.Clear();
                        var opcPeliculas = Utilidades.PedirMenu("Menú Películas.\n1. Ingresar Película \n2. Consultar Película \n3. Consultar Todas las Películas \n0. Volver al Menú Principal", 0, 3);
                        switch (opcPeliculas)
                        {
                            case 0: break;
                            case 1:
                                ControladorPeliculas.IngresarNuevaPelicula();
                                continue;
                            case 2:
                                ControladorPeliculas.ConsultarPeliculaPorId();
                                continue;
                            case 3:
                                ControladorPeliculas.ConsultarTodasLasPeliculas();
                                continue;
                        }
                        continue;
                    case 4:
                        Console.Clear();
                        var opcCopias = Utilidades.PedirMenu("Menú Copias.\n1. Ingresar Copia de Película \n2. Consultar Todas las Copias de Película \n3. Consultar Copias por Película \n0. Volver al Menú Principal", 0, 3);
                        switch (opcCopias)
                        {
                            case 0: break;
                            case 1:
                                ControladorCopias.IngresarNuevaCopia();
                                continue;
                            case 2:
                                ControladorCopias.ConsultarTodasLasCopias();
                                continue;
                            case 3:
                                ControladorCopias.VisualizarReporteCopiasPorPelicula();
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
                                ControladorPrestamos.VisualizarReportePrestamosPorCliente();
                                continue;
                            case 2:
                                ControladorCopias.VisualizarReporteCopiasPorPelicula();
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