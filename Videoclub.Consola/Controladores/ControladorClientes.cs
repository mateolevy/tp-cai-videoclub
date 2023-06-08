using Videoclub.Entidades;
using Videoclub.Negocio;

namespace Videoclub.Consola.Controladores;

internal static class ControladorClientes
{
    internal static void ConsultarClientePorDni()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Cliente por DNI\n");

        try
        {
            while (true)
            {
                var clienteNegocio = new ClienteNegocio();

                // Traemos clientes e imprimimos.
                var clientesResponse = clienteNegocio.ConsultarClientes();

                // Pedimos DNI a del cliente a buscar.
                var dni = Utilidades.PedirDNI("Ingrese el DNI del cliente que desea visualizar:");

                var clienteExistente = clientesResponse.Data.FirstOrDefault(cliente => cliente.Dni.Equals(dni));

                if (clienteExistente != null)
                {
                    Console.Clear();
                    /*
                    Console.WriteLine($"Nombre: {clienteExistente.Nombre} \nApellido: {clienteExistente.Apellido} \nDNI: {clienteExistente.Dni} \nFecha de Nacimiento: {clienteExistente.FechaNacimiento} \nActivo: {clienteExistente.Activo}");
                    */
                    MostrarClienteEnTabla(clienteExistente);
                    Console.WriteLine("\nPresione una tecla para continuar.");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Utilidades.MensajeError($"No existen clientes registrados con el DNI ${dni}.");
                    int opcSeguir = Utilidades.PedirMenu("1. Ingresar otro DNI \n2. Volver al Menú Principal", 1, 2);
                    switch (opcSeguir)
                    {
                        case 1:
                            continue;
                        case 2:
                            break;
                    }
                }
                break;
            }
            
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al consultar cliente existente. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    internal static void ConsultarClientePorTelefono()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Cliente por Telefono\n");

        try
        {
            while (true)
            {
                var clienteNegocio = new ClienteNegocio();

                // Pedimos DNI a del cliente a buscar.
                var telefono = Utilidades.PedirTelefono("Ingrese el telefono del cliente que desea visualizar:");

                var clienteResponse = clienteNegocio.ConsultarClientePorTelefono(telefono);

                if (clienteResponse.Success)
                {
                    Console.Clear();
                    var cliente = clienteResponse.Data;
                    Console.WriteLine("");
                    Console.WriteLine($"Nombre: {cliente.Nombre} \nApellido: {cliente.Apellido} \nDNI: {cliente.Dni} \nFecha de Nacimiento: {cliente.FechaNacimiento.Date} \nActivo: {cliente.Activo}");

                    Console.WriteLine("\nPresione una tecla para continuar.");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Utilidades.MensajeError($"No existen clientes registrados con el telefono {telefono}.");
                    int opcSeguir = Utilidades.PedirMenu("1. Ingresar otro Teléfono \n2. Volver al Menú Principal", 1, 2);
                    switch (opcSeguir)
                    {
                        case 1:
                            continue;
                        case 2:
                            break;
                    }
                }
                break;
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al consultar cliente existente. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
    
    internal static void ConsultarTodosLosClientes()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Todos los Clientes\n");

        try
        {
            var clienteNegocio = new ClienteNegocio();   

            // Traemos clientes e imprimimos.
            var clientesResponse = clienteNegocio.ConsultarClientes();
            List<string> lista;

            // Verificamos si existen clientes.
            if (!clientesResponse.Data.Any())
            {
                Utilidades.MensajeError("No existen clientes registrados. \nPresione una tecla para continuar.");
                Console.ReadKey();
                return;
            }
            /*
            foreach (var cliente in clientesResponse.Data)
            {
                Console.WriteLine($"Nombre: {cliente.Nombre} - Apellido: {cliente.Apellido} - DNI: {cliente.Dni} - Fecha de Nacimiento: {cliente.FechaNacimiento}");
            } 
            */
            MostrarClientesEnTabla(clientesResponse.Data);

            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al consultar todos los cliente. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    internal static void IngresarNuevoCliente()
    {
        Console.Clear();

        try
        {
            var clienteNegocio = new ClienteNegocio();

            // Datos de entrada para nuevo cliente.
            Console.WriteLine("Pantalla de Ingreso de Clientes.\n");

            int dni = Utilidades.PedirDNI("Ingrese su DNI:");
            string nombre = Utilidades.PedirString("Ingrese su Nombre:").ToUpper();
            string apellido = Utilidades.PedirString("Ingrese su Apellido:").ToUpper();
            DateTime fechaNac = Utilidades.PedirFecha("Ingrese su Fecha de Nacimiento:");
            string direccion = Utilidades.PedirString("Ingrese su Dirección:").ToUpper();
            string email = Utilidades.PedirEmail("Ingrese su Email:");
            string telefono = Utilidades.PedirTelefono("Ingrese su Número de Teléfono:");

            // Validamos cliente previo a su registro
            Console.Clear();
            Console.WriteLine("\nSe han ingresado los siguientes datos de cliente: " +
                $"\nDNI: {dni}" +
                $"\nNombre: {nombre}" +
                $"\nApellido: {apellido}" +
                $"\nFecha de Nacimiento: {fechaNac}" +
                $"\nDirección: {direccion}" +
                $"\nEmail: {email}" +
                $"\nTeléfono: {telefono}");
            var opcMenu = Utilidades.PedirMenu("\n1. Continuar 2. Abortar", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nuevo cliente.
                    Cliente nuevoCliente = new Cliente(0, dni, apellido, nombre, fechaNac,  direccion, email, telefono);

                    // Agregamos nuevo cliente y traemos la info sobre como salio la operacion con un booleano.
                    var nuevoClienteResponse = clienteNegocio.AgregarCliente(nuevoCliente);
                    if (nuevoClienteResponse)
                    {
                        Console.Clear();
                        Utilidades.MensajeExito("Cliente agregado con éxito! \nPresione una tecla para continuar.");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de cliente abortado! \nPresione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }            
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError($"\nError al agregar cliente. \nDescripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    private static void MostrarClientesEnTabla(List<Cliente> clientes)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -8}", "Nombre", "Apellido", "DNI", "Fecha Nacimiento\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        foreach (var cliente in clientes)
        {
            Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -8}", cliente.Nombre, cliente.Apellido, cliente.Dni, cliente.FechaNacimiento);
        }
    }

    private static void MostrarClienteEnTabla(Cliente clienteExistente)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -8} | {4, -2}", "Nombre", "Apellido", "DNI", "Fecha Nacimiento", "Activo\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -8} | {4, -2}", clienteExistente.Nombre, clienteExistente.Apellido, clienteExistente.Dni, clienteExistente.FechaNacimiento, clienteExistente.Activo);
    }
}