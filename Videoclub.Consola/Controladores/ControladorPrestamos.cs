using Videoclub.Negocio;
using Videoclub.Entidades;
using Videoclub.AccesoDatos;

namespace Videoclub.Consola.Controladores;

internal class ControladorPrestamos
{
    internal static void ConsultarPrestamosPorPelicula()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Prstamos por Pelicula\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();
            var peliculaDatos = new PeliculaNegocio();
            var copiasDatos = new CopiaNegocio();
            var clienteDatos = new ClienteDatos();

            int idPelicula;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Peliculas:\n");
                
                var peliculasResponse = peliculaDatos.ConsultarPeliculas();
                foreach (var pelicula in peliculasResponse.Data)
                {
                    Console.WriteLine($"Id Pelicula: {pelicula.Id} - Titulo: {pelicula.Titulo}");
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la pelicula:");

                // Validamos el Id de Pelicula ingresado.
                var peliculaResponse = peliculaDatos.ConsultarPeliculaPorId(idPelicula);
                if (peliculaResponse.Success)
                {
                    var pelicula = peliculaResponse.Data;
                    Utilidades.MensajeExito($"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.Id}");
                    int opc = Utilidades.PedirMenu("1. Continuar 2. Eligir nueva pelcula", 1, 2);
                    switch (opc)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"Consulta de Prestamos para la Pelicula: {pelicula.Titulo}:\n");

                            var copiasDePeliculaElegida = copiasDatos.ConsultarCopiasPorIdPelicula(pelicula.Id);

                            if (copiasDePeliculaElegida.Success)
                            {
                                var copias = copiasDePeliculaElegida.Data;
                                var prestamos = prestamoDatos.ConsultarPrestamos();

                                if (prestamos.Success)
                                {
                                    foreach (var copia in copias)
                                    {
                                        var prestamosPorIdCopia = prestamos.Data.Where(prestamo => prestamo.IdCopia.Equals(copia.Id)).ToList();
                                        foreach (var prestamo in prestamosPorIdCopia)
                                        {
                                            var clienteDelPrestamo = clienteDatos.ConsultarClientes().Data.FirstOrDefault(cliente => cliente.Id.Equals(prestamo.IdCliente));
                                            if (clienteDelPrestamo != null)
                                            {
                                                Console.WriteLine($"Id Prestamo: {prestamo.Id} - Fecha Prestamo: {prestamo.FechaPrestamo} - Pelicula: {pelicula.Titulo} Cliente: {clienteDelPrestamo.Nombre} {clienteDelPrestamo.Apellido}");
                                            }
                                        }
                                    }
                                }

                            }
                            break;
                        case 2:
                            break;
                    }
                }
                else
                {
                    Utilidades.MensajeError($"No se encontró el Id de Pelicula: {idPelicula}. \nPresione una tecla para ingresar nueva pelcula.");
                    Console.ReadKey();
                }

                break;
            }

            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar prestamo. \nDescripcion del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    /* Le agregaria a Entidad Copia un atributo que sea la cantidad de copias.
     * Para inresar prestamo:
     1. Buscaria el el cliente por DNI y a partir de ahi asigno el IdCliente
     2. Mostraria lista de peliculas y que el usuario ingrese su nombre
     3. A partir de la pelicula buscaria si la misma tiene copias realizadas o disponibles y a partir de ahi haria efectivo el prestamo.*/
    internal static void IngresarNuevoPrestamo()
    {
        Console.Clear();

        try
        {
            var prestamoDatos = new PrestamoNegocio();

            DateTime fechaPrestamo;
            int idCliente = 0;
            int idCopia = 0;
            int idPelicula;
            int dni;
            string nombreCliente = null;
            string nombrePelicula = null;
                       
            var peliculaDatos = new PeliculaNegocio();
            var copiaDatos = new CopiaNegocio();
            var clienteDatos = new ClienteNegocio();

            // Traemos datos de las pelculas / copias / clientes.
            var peliculasResponse = peliculaDatos.ConsultarPeliculas();
            var clientesResponse = clienteDatos.ConsultarClientes();

            // Pedimos DNI para registrar en el prestamo y validamos que exista.
            Console.WriteLine("Pantalla de Ingreso de Prstamos");
            while (true)
            {
                dni = Utilidades.PedirDNI("Ingrese DNI del cliente:");
                var clientePorDni = clientesResponse.Data.FirstOrDefault(cliente => cliente.Dni.Equals(dni));
                if (clientePorDni != null)
                {
                    idCliente = clientePorDni.Id;
                    nombreCliente = clientePorDni.Nombre + clientePorDni.Apellido;
                }
                else
                {
                    Utilidades.MensajeError($"El cliente con DNI: {dni} no existe. \nPresione una tecla para intentar de nuevo.");
                    Console.ReadKey();
                }
                break;
            }

            // Mostramos pelculas disponibles y pedimos al usuario que ingrese el Id de la misma.   
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Peliculas disponibles:\n");
                foreach (var pelicula in peliculasResponse.Data)
                {
                    Console.WriteLine($"Id Pelicula: {pelicula.Id} - Titulo: {pelicula.Titulo}");
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la pelcula:");
                var peliculaPorId = peliculaDatos.ConsultarPeliculaPorId(idPelicula);
                // Validamos el Id de Pelicula ingresado.
                if (peliculaPorId.Success)
                {
                    var pelicula = peliculaPorId.Data;
                    nombrePelicula = pelicula.Titulo;
                    Utilidades.MensajeExito($"\nSeleccion la pelcula: {pelicula.Titulo} con Id: {pelicula.Id}");
                    int opc = Utilidades.PedirMenu("1. Continuar 2. Eligir nueva pelicula", 1, 2);
                    switch(opc)
                    {
                        case 1: break; 
                        case 2: continue;
                    }
                }
                else
                {
                    Utilidades.MensajeError("No se encontro el Id de Pelicula ingresada. \nPresione una tecla para ingresar nueva pelcula.");
                    Console.ReadKey();
                }

                // Validamos que la pelcula tenga copias
                var copiasPorPeliculaId = copiaDatos.ConsultarCopiasPorIdPelicula(idPelicula);
                if (copiasPorPeliculaId.Success && copiasPorPeliculaId.Data.Any())
                {
                    idCopia = copiasPorPeliculaId.Data.First().Id;
                }
                else
                {
                    Utilidades.MensajeError("Lo sentimos! La pelcula seleccionada no posee copias disponibles. \nPresione una tecla para elegir nuevamente.");
                    Console.ReadKey();
                }
                break;
            }

            // En este punto ya contamos con: IdCliente - IdPelicula - IdCopia

            //Datos de entrada para el nuevo prestamo
            int plazo = Utilidades.PedirInt("Ingrese el Plazo ");
            fechaPrestamo = DateTime.Now;
            DateTime fechaDevolucionTentativa = Utilidades.PedirFecha("Ingrese la Fecha Tentativa de Devolucion");
            DateTime fechaDevolucionReal = Utilidades.PedirFecha("Ingrese la Fecha Real de la Devolucion");

            // Validamos prestamo previo a su ingreso
            Console.WriteLine("\nSe han ingresado los siguientes datos de prestamo:" +
                $"\nCliente: {nombreCliente} con DNI: {dni}" +
                $"\nPelicula: {nombrePelicula} con Id: {idPelicula}" +
                $"\nPlazo: {plazo}" +
                $"\nFecha Tentativa de Devolucin: {fechaDevolucionTentativa}" +
                $"\nFecha Real de Devolucin:{fechaDevolucionReal}");
            int opcMenu = Utilidades.PedirMenu("1. Continuar 2. Abortar", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nuevo prestamo
                    Prestamo nuevoPrestamo = new Prestamo(0, idCliente, idCopia, plazo, fechaPrestamo, fechaDevolucionTentativa, fechaDevolucionReal);

                    //Agregamos prestamo e informamos si se realiz correctamente o no 
                    var nuevoPrestamoResponse = prestamoDatos.AgregarPrestamo(nuevoPrestamo);
                    if (nuevoPrestamoResponse)
                    {
                        Console.Clear();
                        Utilidades.MensajeExito("\nPrstamo agregado con exito! \nPresione una tecla para continuar.");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de prestamo abortado! \nPresione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al agregar prestamo. Descripcion del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }

    }
    
    internal static void VisualizarReportePrestamosPorCliente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Reporte de Prestamos por Cliente\n");

        try
        {
            var prestamoNegocio = new PrestamoNegocio();
            var clienteNegocio = new ClienteNegocio();
            var peliculaNegocio = new PeliculaNegocio();
            var copiasNegocio = new CopiaNegocio();
            
            // Traemos clientes
            var clientesResponse = clienteNegocio.ConsultarClientes();

            int idCliente = 0;
            string? nombreCliente = null;

            // Pedimos DNI del cliente y buscamos su Id Cliente para luego buscar Prestamos.
            while (true)
            {
                var dni = Utilidades.PedirDNI("Ingrese DNI del cliente:");
                
                var clienteExistente = clientesResponse.Data.FirstOrDefault(cliente => cliente.Dni.Equals(dni));
                if (clienteExistente != null)
                {
                    idCliente = clienteExistente.Id;
                    nombreCliente = clienteExistente.NombreCompleto;
                }
                else
                {
                    Utilidades.MensajeError($"El cliente con DNI: {dni} no existe. \nPresione una tecla para intentar de nuevo.");
                    Console.ReadKey();
                }
                
                // Buscamos prestamos asociados al Id Cliente
                var prestamosResponse = prestamoNegocio.ConsultarPrestamos();

                var prestamosExistentes = prestamosResponse.Data.Where(cliente => cliente.IdCliente.Equals(idCliente)).ToList();

                if (prestamosExistentes.Any())
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Reporte de prestamos del cliente: {nombreCliente}. \nTotal de prestamos encontrados: {prestamosExistentes.Count}");
                    Console.WriteLine("");
                    
                    var copiasResponse = copiasNegocio.ConsultarCopias();

                    foreach (var prestamo in prestamosExistentes)
                    {
                        var copia = copiasResponse.Data.FirstOrDefault(copia => copia.Id.Equals(prestamo.IdCopia));
                        if (copia == null) continue;
                        
                        var pelicula = peliculaNegocio.ConsultarPeliculaPorId(copia.IdPelicula);
                        Console.WriteLine($"Fecha Prestamo: {prestamo.FechaPrestamo} - Fecha Devolucion Tent.: {prestamo.FechaPrestamo.Date} - Plazo: {prestamo.Plazo} días - Pelicula: {pelicula.Data.Titulo} - Id de Copia: {copia.Id}\n");
                    }
                }
                else
                {
                    Utilidades.MensajeError("\nNo se encontraron prestamos asociados a ese cliente.");
                }
                break;
            }

            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar report de prestamos por cliente. \nDescripcion del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }                
    }
}