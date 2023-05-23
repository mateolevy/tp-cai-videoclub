using System.ComponentModel.Design;
using Videoclub.Entidades;
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


        /*********************************************************** COPIAS ************************************************************************/
        private static void ConsultarCopiaPeliculaExistente()
        {
            throw new NotImplementedException();
        }
        private static void IngresarCopiaPelicula()
        {
            throw new NotImplementedException();
        }
        private static void VisualizarCopiasPelicula()
        {
            throw new NotImplementedException();
        }
        /*********************************************************** PELICULAS ************************************************************************/
        private static void ConsultarPeliculaExistente()
        {
            throw new NotImplementedException();
        }

        private static void IngresarNuevaPelicula()
        {
            throw new NotImplementedException();
        }
        /*********************************************************** PRESTAMOS ************************************************************************/
        private static void ConsultarPrestamoExistente()
        {
            throw new NotImplementedException();
        }

        private static void IngresarNuevoPrestamo()
        {
            throw new NotImplementedException();
        }
        private static void VisualizarPrestamosCliente()
        {
            throw new NotImplementedException();
        }
        /*********************************************************** CLIENTES ************************************************************************/
        private static void ConsultarClienteExistente()
        {
            Console.Clear();
            Console.WriteLine("Pantalla de Consulta de Cliente\n");

            var clienteDatos = new ClienteNegocio();

            // Verificamos si existen clientes.
            if (clienteDatos.ExistenClientesIngresados())
            {
                // Traemos clientes e imprimimos.
                var clientesResponse = clienteDatos.ConsultarClientes();

                // Pedimos DNI a del cliente a buscar.
                int dni = Utilidades.PedirDNI("Ingrese el DNI del cliente que desea visualizar:");
                foreach (var cliente in clientesResponse.Data)
                {
                    if (cliente.Dni == dni)
                    {
                        Console.WriteLine("Nombre: {cliente.Nombre} \nApellido: {cliente.Apellido} \nDNI: {cliente.Dni} \nFecha de Nacimiento: {cliente.FechaNacimiento} \nActivo: {cliente.Activo}");
                    }
                    else
                    {
                        Console.WriteLine("\nNo existe un cliente registrado bajo el DNI ingresado.");
                    }
                    break;
                    
                }
                Console.WriteLine("\nPresione una tecla para continuar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No existen clientes registrados. \nPresione una tecla para continuar.");
                Console.ReadKey();
            }
        }
        
        private static void ConsultarTodosLosClientes()
        {
            Console.Clear();
            Console.WriteLine("Pantalla de Consulta de Todos los Clientes\n");

            var clienteDatos = new ClienteNegocio();   
            
            // Verificamos si existen clientes.
            if (clienteDatos.ExistenClientesIngresados())
            {
                // Traemos clientes e imprimimos.
                var clientesResponse = clienteDatos.ConsultarClientes();
                foreach (var cliente in clientesResponse.Data)
                {
                    Console.WriteLine($"Nombre: {cliente.Nombre} - Apellido: {cliente.Apellido} - DNI: {cliente.Dni} - Fecha de Nacimiento: {cliente.FechaNacimiento}");
                }
                Console.WriteLine("\nPresione una tecla para continuar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No existen clientes registrados. \nPresione una tecla para continuar.");
                Console.ReadKey();
            }
        }

        private static void IngresarNuevoCliente()
        {
            try
            {
                Console.Clear();

                var clienteDatos = new ClienteNegocio();
                int idCliente = 1;

                // Verificamos si existen clientes.
                if (clienteDatos.ExistenClientesIngresados())
                {
                    // Traemos clientes y buscamos el ID mas alto.
                    var clientes = clienteDatos.ConsultarClientes();
                    int maxId = 0;
                    foreach (var cliente in clientes.Data)
                    {
                        if (cliente.Id > maxId)
                        {
                            maxId = cliente.Id;
                        }
                    }
                    idCliente = maxId + 1;
                }

                // Datos de entrada para nuevo cliente.
                Console.WriteLine("Pantalla de Ingreso de Clientes.\n");
                int dni = Utilidades.PedirDNI("Ingrese su DNI:");
                string nombre = Utilidades.PedirString("Ingrese su Nombre:").ToUpper();
                string apellido = Utilidades.PedirString("Ingrese su Apellido:").ToUpper();
                DateTime fechaNac = Utilidades.PedirFecha("Ingrese su Fecha de Nacimiento:");
                string direccion = Utilidades.PedirString("Ingrese su Dirección:").ToUpper();
                string email = Utilidades.PedirString("Ingrese su Email:");
                string telefono = Utilidades.PedirTelefono("Ingrese su Número de Teléfono:");
                string usuario = Utilidades.PedirString("Ingrese su Usuario:");
                bool activo = true;

                // Instanciamos nuevo cliente.
                Cliente nuevoCliente = new Cliente(dni, apellido, nombre, fechaNac, idCliente, DateTime.Today, direccion, email, telefono, usuario, activo);
                
                // Agregamos nuevo cliente y traemos la info sobre como salio la operacion con un booleano.
                var nuevoClienteResponse = clienteDatos.AgregarCliente(nuevoCliente);
                if (nuevoClienteResponse)
                {
                    Console.WriteLine("\nCliente agregado con éxito! \nPresione una tecla para continuar.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex} \nDescripción del Error: {ex.Message}.");
            }
            
        }
    }
}