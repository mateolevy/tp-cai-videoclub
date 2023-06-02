using Videoclub.Negocio;
using Videoclub.Entidades;
using Videoclub.AccesoDatos;

namespace Videoclub.Consola.Controladores;

internal class ControladorPrestamos
{
    internal static void ConsultarPrestamoExistente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Prstamos por Pelicula\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();
            var peliculaDatos = new PeliculaNegocio();
            var copiasDatos = new CopiaNegocio();
            var clienteDatos = new ClienteDatos();

            // Traemos prestamos y peliculas
            var prestamoResponse = prestamoDatos.ConsultarPrestamos();
            var peliculasResponse = peliculaDatos.ConsultarPeliculas();
            var copiasResponse = copiasDatos.ConsultarCopias();
            var clientesResponse = clienteDatos.ConsultarClientes();

            int idPelicula;
            string nombrePelicula;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Peliculas:\n");
                foreach (var pelicula in peliculasResponse.Data)
                {
                    Console.WriteLine($"Id Pelicula: {pelicula.IdPelicula} - Titulo: {pelicula.Titulo}");
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la pelcula:");

                // Validamos el Id de Pelicula ingresado.
                foreach (var pelicula in peliculasResponse.Data)
                {
                    if (pelicula.IdPelicula.Equals(idPelicula))
                    {
                        nombrePelicula = pelicula.Titulo;
                        Utilidades.MensajeExito($"\nSeleccion la pelcula: {pelicula.Titulo} con Id: {pelicula.IdPelicula}");
                        int opc = Utilidades.PedirMenu("1. Continuar 2. Eligir nueva pelcula", 1, 2);
                        switch (opc)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine($"Consulta de Prstamos para la Pelicula: {nombrePelicula}:\n");
                                foreach (var copia in copiasResponse.Data)
                                {
                                    if (copia.IdPelicula.Equals(idPelicula))
                                    {
                                        foreach (var prestamo in prestamoResponse.Data)
                                        {
                                            if (prestamo.IdCopia.Equals(copia.IdCopia))
                                            {
                                                foreach (var cliente in clientesResponse.Data)
                                                {
                                                    if (cliente.Id.Equals(prestamo.IdCliente))
                                                    {
                                                        Console.WriteLine($"Fecha: {prestamo.FechaPrestamo} - Cliente: {cliente.Nombre} {cliente.Apellido}");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2: continue;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError($"No se encontr el Id de Pelicula: {idPelicula}. \nPresione una tecla para ingresar nueva pelcula.");
                        Console.ReadKey();
                    }
                }
                break;
            }

            /* INICIO CODIGO VIEJO
            // Pedimos ID de pelicula para buscar el prestamo 
            var idPelicula = Utilidades.PedirInt("Ingrese el ID de la Pelicula para Ver los Prstamos Asociados");
            foreach( var prestamo in prestamoResponse.Data )
            {
                if(prestamo.IdCopia.Equals(idPelicula))
                {
                    Console.WriteLine($"El cliente con el ID: {prestamo.IdCliente} realiz el prestamo el da {prestamo.FechaPrestamo}\n");
                }
                else
                {
                    Console.WriteLine("\nNo se encontraron prestamos asociados a ese ID");
                }
                break;
            }
            FIN CODIGO VIEJO*/
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
            var copiasResponse = copiaDatos.ConsultarCopias();
            var clientesResponse = clienteDatos.ConsultarClientes();
            var prestamosResponse = prestamoDatos.ConsultarPrestamos();

            // Pedimos DNI para registrar en el prestamo y validamos que exista.
            Console.WriteLine("Pantalla de Ingreso de Prstamos");
            while (true)
            {
                dni = Utilidades.PedirDNI("Ingrese DNI del cliente:");
                foreach (var cliente in clientesResponse.Data)
                {
                    //Consultamos el cliente exista y nos devuelve el IdCliente 
                    if (cliente.Dni.Equals(dni))
                    {
                        idCliente = cliente.Id;
                        nombreCliente = cliente.Nombre + cliente.Apellido;
                    }
                    else
                    {
                        Utilidades.MensajeError($"El cliente con DNI: {dni} no existe. \nPresione una tecla para intentar de nuevo.");
                        Console.ReadKey();
                        continue;
                    }
                    break;
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
                    Console.WriteLine($"Id Pelicula: {pelicula.IdPelicula} - Titulo: {pelicula.Titulo}");
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la pelcula:");
                // Validamos el Id de Pelicula ingresado.
                foreach (var pelicula in peliculasResponse.Data)
                {
                    if (pelicula.IdPelicula.Equals(idPelicula))
                    {
                        nombrePelicula = pelicula.Titulo;
                        Utilidades.MensajeExito($"\nSeleccion la pelcula: {pelicula.Titulo} con Id: {pelicula.IdPelicula}");
                        int opc = Utilidades.PedirMenu("1. Continuar 2. Eligir nueva pelicula", 1, 2);
                        switch(opc)
                        {
                            case 1: break; 
                            case 2: continue;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError("No se encontr el Id de Pelicula ingresado. \nPresione una tecla para ingresar nueva pelcula.");
                        Console.ReadKey();
                    }
                }
                // Validamos que la pelcula tenga copias
                foreach (var copia in copiasResponse.Data)
                {
                    if (copia.IdPelicula.Equals(idPelicula))
                    {
                        idCopia = copia.IdCopia;
                        copia.CopiasDisponibles --;
                    }
                    else
                    {
                        Utilidades.MensajeError("Lo sentimos! La pelcula seleccionada no posee copias disponibles. \nPresione una tecla para elegir nuevamente.");
                        Console.ReadKey();
                        continue;
                    }
                    break;
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
    internal static void VisualizarPrestamosCliente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Prstamos por Cliente\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();
            var clienteDatos = new ClienteNegocio();
            var copiaDatos = new CopiaNegocio();
            var peliculaDatos = new PeliculaNegocio();
            
            // Traemos prestamos y clientes
            var prestamosResponse = prestamoDatos.ConsultarPrestamos();
            var clientesResponse = clienteDatos.ConsultarClientes();
            var copiasResponse = copiaDatos.ConsultarCopias();
            var peliculasResponse = peliculaDatos.ConsultarPeliculas();

            int idCliente = 0;
            string? clienteNombre = null;


            // Pedimos DNI del cliente y buscamos su Id Cliente para luego buscar Prestamos.
            while (true)
            {
                var dni = Utilidades.PedirDNI("Ingrese DNI del cliente:");
                foreach (var cliente in clientesResponse.Data)
                {
                    //Consultamos el cliente exista y nos devuelve el IdCliente 
                    if (cliente.Dni.Equals(dni))
                    {
                        idCliente = cliente.Id;
                        clienteNombre = cliente.Nombre + cliente.Apellido;
                    }
                    else
                    {
                        Utilidades.MensajeError($"El cliente con DNI: {dni} no existe. \nPresione una tecla para intentar de nuevo.");
                        Console.ReadKey();
                        continue;
                    }
                    break;
                }
                break;
            }

            // Buscamos prestamos asociados al Id Cliente
            var contadorPrestamos = 0;
            foreach (var prestamo in prestamosResponse.Data)
            {
                if (prestamo.IdCliente.Equals(idCliente))
                {
                    foreach (var pelicula in peliculasResponse.Data.Where(pelicula => pelicula.IdPelicula.Equals(pelicula.IdPelicula)))
                    {
                        contadorPrestamos++;
                        Console.WriteLine($"Prestamos del Cliente: {clienteNombre} \nNro de Prestamo: {contadorPrestamos} - Fecha Prestamo: {prestamo.FechaPrestamo} - Pelicula: {pelicula.Titulo}\n");
                    }
                }
                else
                {
                    Utilidades.MensajeError("\nNo se encontraron prestamos asociados a ese ID.");
                }
            }

            /* CODIGO VIEJO
            foreach ( var prestamo in prestamosResponse.Data)
            {
                if(prestamo.IdCliente.Equals(idCliente))
                {
                    foreach(var cliente in clientesResponse.Data)
                    {
                        if(cliente.Id.Equals(prestamo.IdCliente))
                        {
                            Console.WriteLine($"El cliente {cliente.Nombre} {cliente.Apellido} solicit un prestamo el da: {prestamo.FechaPrestamo} de la pelicula: 'pelicula.Titulo'\n");
                        }
                    }
                }
                else
                {
                    Utilidades.MensajeError("\nNo se encontraron prestamos asociados a ese ID.");
                }
            }
            FIN CODIGO VIEJO */ 

            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();

        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar prestamo. \nDescripcion del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }                
    }
}