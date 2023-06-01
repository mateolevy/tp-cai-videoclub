using Videoclub.Negocio;
using Videoclub.Entidades;
using Videoclub.Negocio.Excepciones;
using System.Reflection.Metadata.Ecma335;
using Videoclub.AccesoDatos;

namespace Videoclub.Consola.Controladores;

internal class ControladorPrestamos
{
    internal static void ConsultarPrestamoExistente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Préstamos\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();

            // Traemos prestamos
            var prestamoResponse = prestamoDatos.ConsultarPrestamos();
            
            // Pedimos ID de pelicula para buscar el prestamo 
            var idPelicula = Utilidades.PedirInt("Ingrese el ID de la Película para Ver los Préstamos Asociados");
            foreach( var prestamo in prestamoResponse.Data )
            {
                if(prestamo.IdCopia.Equals(idPelicula))
                {
                    Console.WriteLine($"El cliente con el ID: {prestamo.IdCliente} realizó el préstamo el día {prestamo.FechaPrestamo}\n");
                }
                else
                {
                    Console.WriteLine("\nNo se encontraron préstamos asociados a ese ID");
                }
                break;
            }
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar préstamo. \nDescripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    /* Le agregaría a Entidad Copia un atributo que sea la cantidad de copias.
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
            int idPrestamo;
            int idPelicula;
            int dni;
            string nombreCliente = null;
            string nombrePelicula = null;
                       
            var peliculaDatos = new PeliculaNegocio();
            var copiaDatos = new CopiaNegocio();
            var clienteDatos = new ClienteNegocio();

            // Traemos datos de las películas / copias / clientes.
            var peliculasResponse = peliculaDatos.ConsultarPeliculas();
            var copiasResponse = copiaDatos.ConsultarCopias();
            var clientesResponse = clienteDatos.ConsultarClientes();
            var prestamosResponse = prestamoDatos.ConsultarPrestamos();

            // Pedimos DNI para registrar en el préstamo y validamos que exista.
            Console.WriteLine("Pantalla de Ingreso de Préstamos");
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

            // Mostramos películas disponibles y pedimos al usuario que ingrese el Id de la misma.   
            while (true)
            {
                Console.Clear();
                foreach (var pelicula in peliculasResponse.Data)
                {
                    Console.WriteLine($"Id Pelicula: {pelicula.IdPelicula} - Titulo: {pelicula.Titulo}");
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la película:");
                // Validamos el Id de Pelicula ingresado.
                foreach (var pelicula in peliculasResponse.Data)
                {
                    if (pelicula.IdPelicula.Equals(idPelicula))
                    {
                        nombrePelicula = pelicula.Titulo;
                        Utilidades.MensajeExito($"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.IdPelicula}");
                        int opc = Utilidades.PedirMenu("1. Continuar 2. Eligir nueva película", 1, 2);
                        switch(opc)
                        {
                            case 1: break; 
                            case 2: continue;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError("No se encontró el Id de Película ingresado. \nPresione una tecla para ingresar nueva película.");
                        Console.ReadKey();
                        continue;
                    }
                }
                // Validamos que la película tenga copias
                foreach (var copia in copiasResponse.Data)
                {
                    if (copia.IdPelicula.Equals(idPelicula))
                    {
                        idCopia = copia.IdCopia;
                        copia.CopiasDisponibles --;
                    }
                    else
                    {
                        Utilidades.MensajeError("Lo sentimos! La película seleccionada no posee copias disponibles. \nPresione una tecla para elegir nuevamente.");
                        Console.ReadKey();
                        continue;
                    }
                    break;
                }
                break;
            }

            // En este punto ya contamos con: IdCliente - IdPelicula - IdCopia

            // Buscamos ID mas alto de préstamos
            int maxId = 0;
            Console.Clear();
            foreach (var prestamo in prestamosResponse.Data)
            {
                if(prestamo.IdPrestamo > maxId)
                {
                    maxId = prestamo.IdPrestamo;
                }
            } 
            idPrestamo = maxId + 1;

            //Datos de entrada para el nuevo prestamo
            int plazo = Utilidades.PedirInt("Ingrese el Plazo ");
            fechaPrestamo = DateTime.Now;
            DateTime fechaDevolucionTentativa = Utilidades.PedirFecha("Ingrese la Fecha Tentativa de Devolucion");
            DateTime fechaDevolucionReal = Utilidades.PedirFecha("Ingrese la Fecha Real de la Devolucion");

            // Validamos préstamo previo a su ingreso
            Console.WriteLine("\nSe han ingresado los siguientes datos de préstamo:" +
                $"\nCliente: {nombreCliente} con DNI: {dni}" +
                $"\nPelícula: {nombrePelicula} con Id: {idPelicula}" +
                $"\nPlazo: {plazo}" +
                $"\nFecha Tentativa de Devolución: {fechaDevolucionTentativa}" +
                $"\nFecha Real de Devolución:{fechaDevolucionReal}");
            int opcMenu = Utilidades.PedirMenu("1. Continuar 2. Abortar", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nuevo prestamo
                    Prestamo nuevoPrestamo = new Prestamo(idPrestamo, idCliente, idCopia, plazo, fechaPrestamo, fechaDevolucionTentativa, fechaDevolucionReal);

                    //Agregamos prestamo e informamos si se realizó correctamente o no 
                    var nuevoPrestamoResponse = prestamoDatos.AgregarPrestamo(nuevoPrestamo);
                    if (nuevoPrestamoResponse)
                    {
                        Console.Clear();
                        Utilidades.MensajeExito("\nPréstamo agregado con éxito! \nPresione una tecla para continuar.");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de préstamo abortado! \nPresione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al agregar préstamo. Descripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }

    }
    internal static void VisualizarPrestamosCliente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Préstamos por Cliente\n");

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
            string clienteNombre = null;


            // Pedimos DNI del cliente y buscamos su Id Cliente para luego buscar Prestamos.
            var dni = Utilidades.PedirDNI("Por favor ingrese el DNI del cliente para ver sus préstamos:");
            while (true)
            {
                dni = Utilidades.PedirDNI("Ingrese DNI del cliente:");
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
            int contadorPrestamos = 0;
            foreach (var prestamo in prestamosResponse.Data)
            {
                if (prestamo.IdCliente.Equals(idCliente))
                {
                    foreach (var pelicula in peliculasResponse.Data)
                    {
                        if (pelicula.IdPelicula.Equals(pelicula.IdPelicula))
                        {
                            contadorPrestamos++;
                            Console.WriteLine($"Préstamos del Cliente: {clienteNombre} \nNro de Préstamo: {contadorPrestamos} - Fecha Préstamo: {prestamo.FechaPrestamo} - Película: {pelicula.Titulo}\n");
                        }
                    }
                }
                else
                {
                    Utilidades.MensajeError("\nNo se encontraron préstamos asociados a ese ID.");
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
                            Console.WriteLine($"El cliente {cliente.Nombre} {cliente.Apellido} solicitó un préstamo el déa: {prestamo.FechaPrestamo} de la pelicula: 'pelicula.Titulo'\n");
                        }
                    }
                }
                else
                {
                    Utilidades.MensajeError("\nNo se encontraron préstamos asociados a ese ID.");
                }
            }
            FIN CODIGO VIEJO */ 

            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();

        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar préstamo. \nDescripción del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }                
    }
}