using Videoclub.Negocio;

namespace Videoclub.Consola
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
			try
			{
				while (true)
				{
                    Console.Clear();
                    int opcMenu = Utilidades.PedirMenu("Bienvenido al sistema de gestión 'Video Club'!\n" +
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
                            int opcClientes = Utilidades.PedirMenu("Menú Clientes.\n1. Ingresar Nuevo Cliente \n2. Consultar Cliente Existente \n3. Consultar Todos los Clientes \n0. Volver al Menú Principal", 0, 3);
							switch (opcClientes)
							{
								case 0: break;
								case 1:
									IngresarNuevoCliente();
									continue;
								case 2:
									ConsultarClienteExistente();
									continue;
                                case 3:
                                    ConsultarTodosLosClientes();
                                    continue;
							}
							continue;
						case 2:
                            Console.Clear();
                            int opcPrestamos = Utilidades.PedirMenu("Menú Préstamos.\n1. Ingresar Préstamo \n2. Consultar Préstamo \n0. Volver al Menú Principal", 0, 2);
                            switch (opcPrestamos)
                            {
                                case 0: break;
                                case 1:
                                    IngresarNuevoPrestamo();
                                    continue;
                                case 2:
                                    ConsultarPrestamoExistente();
                                    continue;
                            }
                            continue;
                        case 3:
                            Console.Clear();
                            int opcPeliculas = Utilidades.PedirMenu("Menú Películas.\n1. Ingresar Película \n2. Consultar Película \n0. Volver al Menú Principal", 0, 2);
                            switch (opcPeliculas)
                            {
                                case 0: break;
                                case 1:
                                    IngresarNuevaPelicula();
                                    continue;
                                case 2:
                                    ConsultarPeliculaExistente();
                                    continue;
                            }
                            continue;
                        case 4:
                            Console.Clear();
                            int opcCopias = Utilidades.PedirMenu("Menú Copias.\n1. Ingresar Copia de Película \n2. Consultar Copia de Película \n0. Volver al Menú Principal", 0, 2);
                            switch (opcCopias)
                            {
                                case 0: break;
                                case 1:
                                    IngresarCopiaPelicula();
                                    continue;
                                case 2:
                                    ConsultarCopiaPeliculaExistente();
                                    continue;
                            }
                            continue;
                        case 5:
                            Console.Clear();
                            int opcReportes = Utilidades.PedirMenu("Menú Reportes.\n 1. Visualizar Préstamos por CLiente \n2. Visualizar Copias por Película \n0. Volver al Menú Principal", 0, 2);
                            switch (opcReportes)
                            {
                                case 0: break;
                                case 1:
                                    VisualizarPrestamosCliente();
                                    continue;
                                case 2:
                                    VisualizarCopiasPelicula();
                                    continue;
                            }
                            continue;
                    }
				}
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Error: {ex} \nDescripción del Error: {ex.Message}.");
			}
        }

        private static void VisualizarCopiasPelicula()
        {
            throw new NotImplementedException();
        }

        private static void VisualizarPrestamosCliente()
        {
            throw new NotImplementedException();
        }

        private static void ConsultarCopiaPeliculaExistente()
        {
            throw new NotImplementedException();
        }

        private static void IngresarCopiaPelicula()
        {
            throw new NotImplementedException();
        }

        private static void ConsultarPeliculaExistente()
        {
            throw new NotImplementedException();
        }

        private static void IngresarNuevaPelicula()
        {
            throw new NotImplementedException();
        }

        private static void ConsultarPrestamoExistente()
        {
            throw new NotImplementedException();
        }

        private static void IngresarNuevoPrestamo()
        {
            throw new NotImplementedException();
        }

        private static void ConsultarClienteExistente()
        {
            throw new NotImplementedException();
        }
        
        private static void ConsultarTodosLosClientes()
        {
            var clienteDatos = new ClienteNegocio();
            var clientesResponse = clienteDatos.ConsultarClientes();

            if (clientesResponse.Success)
            {
                Console.WriteLine("\n Listado de Clientes: \n");
                foreach (var cliente in clientesResponse.Data)
                {
                    Console.WriteLine($"{cliente.Nombre} {cliente.Apellido} - {cliente.Dni} - {cliente.FechaNacimiento}");
                }
            }
            else
            {
                Console.WriteLine(clientesResponse.Error);
            }
        }

        private static void IngresarNuevoCliente()
        {
            throw new NotImplementedException();
        }
    }
}