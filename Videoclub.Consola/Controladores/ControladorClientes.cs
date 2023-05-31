using Videoclub.Entidades;
using Videoclub.Negocio;

namespace Videoclub.Consola.Controladores;

internal static class ControladorClientes
{
    internal static void ConsultarClienteExistente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Cliente\n");

        try
        {
            var clienteDatos = new ClienteNegocio();

            // Traemos clientes e imprimimos.
            var clientesResponse = clienteDatos.ConsultarClientes();

            // Pedimos DNI a del cliente a buscar.
            var dni = Utilidades.PedirDNI("Ingrese el DNI del cliente que desea visualizar:");
            foreach (var cliente in clientesResponse.Data)
            {
                if (cliente.Dni.Equals(dni))
                {
                    Console.WriteLine($"Nombre: {cliente.Nombre} \nApellido: {cliente.Apellido} \nDNI: {cliente.Dni} \nFecha de Nacimiento: {cliente.FechaNacimiento} \nActivo: {cliente.Activo}");
                }
                else
                {
                    Utilidades.MensajeError("No existe un cliente registrado bajo el DNI ingresado.");
                }
                break;
            }
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
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
            var clienteDatos = new ClienteNegocio();   

            // Traemos clientes e imprimimos.
            var clientesResponse = clienteDatos.ConsultarClientes();
        
            // Verificamos si existen clientes.
            if (!clientesResponse.Data.Any())
            {
                Utilidades.MensajeError("No existen clientes registrados. \nPresione una tecla para continuar.");
                Console.ReadKey();
                return;
            }

            foreach (var cliente in clientesResponse.Data)
            {
                Console.WriteLine($"Nombre: {cliente.Nombre} - Apellido: {cliente.Apellido} - DNI: {cliente.Dni} - Fecha de Nacimiento: {cliente.FechaNacimiento}");
            }
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
            var clienteDatos = new ClienteNegocio();
            int idCliente;

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
            int opcMenu = Utilidades.PedirMenu("\n1. Continuar 2. Abortar", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nuevo cliente.
                    Cliente nuevoCliente = new Cliente(dni, apellido, nombre, fechaNac, idCliente, direccion, email, telefono);

                    // Agregamos nuevo cliente y traemos la info sobre como salio la operacion con un booleano.
                    var nuevoClienteResponse = clienteDatos.AgregarCliente(nuevoCliente);
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
            Utilidades.MensajeError($"\nError al agregar cliente. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
}