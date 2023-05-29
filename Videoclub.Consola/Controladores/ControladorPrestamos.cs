using Videoclub.Negocio;
using Videoclub.Entidades;

namespace Videoclub.Consola.Controladores;

internal class ControladorPrestamos
{
    internal static void ConsultarPrestamoExistente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Prestamos\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();

            // Traemos prestamos
            var prestamoResponse = prestamoDatos.ConsultarPrestamos();
            
            // Pedimos ID de pelicula para buscar el prestamo 
            var idPelicula = Utilidades.PedirInt("Ingrese el ID de la Pelicula para Ver los Prestamos Asociados");
            bool prestamosEncontrados = false;
            foreach( var prestamo in prestamoResponse.Data )
            {
                if(prestamo.IdCopia.Equals(idPelicula))
                {
                    Console.WriteLine($"El cliente con el ID: {prestamo.IdCliente} realizó el prestamo el dia {prestamo.FechaPrestamo}\n");
                    prestamosEncontrados = true;
                }           
            }
            if (!prestamosEncontrados)
            {
                Console.WriteLine("No se encontraron prestamos asociados a ese ID");
            }
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(" ");
            Console.WriteLine($"Error al consultar prestamo. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    internal static void IngresarNuevoPrestamo()
    {
        Console.Clear();

        try
        {
            var prestamoDatos = new PrestamoNegocio();
            int IdPrestamo;
            DateTime fechaPrestamo = DateTime.Now;
            

            //Traemos prestamos y buscamos ID mas alto
            var prestamos = prestamoDatos.ConsultarPrestamos();
            int maxId = 0;  
            foreach (var prestamo in prestamos.Data)
            {
                if(prestamo.IdPrestamo > maxId)
                {
                    maxId = prestamo.IdPrestamo;
                }
            } 
            IdPrestamo = maxId + 1;

            //Datos de entrada para el nuevo prestamo
            Console.WriteLine("Pantalla de Ingreso de Prestamos");
            int idCliente = Utilidades.PedirInt("Ingrese el ID del Cliente que Solicita el Prestamo");
            int idCopia = Utilidades.PedirInt("Ingrese el ID de la Pelicula");
            int plazo = Utilidades.PedirInt("Ingrese el Plazo ");
            DateTime fechaDevolucionTentativa = Utilidades.PedirFecha("Ingrese la Fecha Tentativa de Devolucion");
            DateTime fechaDevolucionReal = Utilidades.PedirFecha("Ingrese la Fecha Real de la Devolucion");

            // Instanciamos nuevo prestamo
            Prestamo nuevoPrestamo = new Prestamo(IdPrestamo, idCliente, idCopia, plazo, fechaPrestamo, fechaDevolucionTentativa, fechaDevolucionReal);

            //Agregamos prestamo e informamos si se realizó correctamente o no 
            var nuevoPrestamoResponse = prestamoDatos.AgregarPrestamo(nuevoPrestamo);
            if (nuevoPrestamoResponse)
            {
                Console.WriteLine("\nPrestamo agregado con éxito! \nPresione una tecla para continuar.");
                Console.ReadKey();
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine(" ");
            Console.WriteLine($"Error al agregar prestamo. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }

    }
    internal static void VisualizarPrestamosCliente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Prestamos por Cliente\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();
            var clienteDatos = new ClienteNegocio();
            
            // Traemos prestamos y clientes
            var prestamoResponse = prestamoDatos.ConsultarPrestamos();
            var clienteResponse = clienteDatos.ConsultarClientes();

            // Pedimos ID cliente y buscamos los prestamos asociados
            var IdCliente = Utilidades.PedirInt("Por favor ingrese el ID del cliente para ver sus prestamos");
            bool prestamosEncontrados = false;
            foreach ( var prestamo in prestamoResponse.Data)
            {
                if(prestamo.IdCliente == IdCliente)
                {
                    foreach(var cliente in clienteResponse.Data)
                    {
                        if(cliente.Id == prestamo.IdCliente)
                        {
                            Console.WriteLine($"El cliente {cliente.Nombre} {cliente.Apellido} solicitó un prestamo el dia: {prestamo.FechaPrestamo} de la pelicula con el ID numero: {prestamo.IdCopia}\n");
                            prestamosEncontrados = true;
                        }
                    }

                    
                }
            }
            if (!prestamosEncontrados)
            {
                Console.WriteLine("No se encontraron prestamos asociados a ese ID");
            }
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();

        }
        catch (Exception ex) 
        {
            Console.WriteLine(" ");
            Console.WriteLine($"Error al consultar prestamo. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }                
    }
}