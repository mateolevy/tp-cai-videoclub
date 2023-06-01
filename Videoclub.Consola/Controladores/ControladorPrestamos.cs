using Videoclub.Negocio;
using Videoclub.Entidades;
using Videoclub.Negocio.Excepciones;
using System.Reflection.Metadata.Ecma335;

namespace Videoclub.Consola.Controladores;

internal class ControladorPrestamos
{
    internal static void ConsultarPrestamoExistente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Pr�stamos\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();

            // Traemos prestamos
            var prestamoResponse = prestamoDatos.ConsultarPrestamos();
            
            // Pedimos ID de pelicula para buscar el prestamo 
            var idPelicula = Utilidades.PedirInt("Ingrese el ID de la Pel�cula para Ver los Pr�stamos Asociados");
            foreach( var prestamo in prestamoResponse.Data )
            {
                if(prestamo.IdCopia.Equals(idPelicula))
                {
                    Console.WriteLine($"El cliente con el ID: {prestamo.IdCliente} realiz� el pr�stamo el d�a {prestamo.FechaPrestamo}\n");
                }
                else
                {
                    Console.WriteLine("\nNo se encontraron pr�stamos asociados a ese ID");
                }
                break;
            }
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar pr�stamo. \nDescripci�n del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    /* Le agregar�a a Entidad Copia un atributo que sea la cantidad de copias.
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

            // Traemos datos de las pel�culas / copias / clientes.
            var peliculasResponse = peliculaDatos.ConsultarPeliculas();
            var copiasResponse = copiaDatos.ConsultarCopias();
            var clientesResponse = clienteDatos.ConsultarClientes();
            var prestamosResponse = prestamoDatos.ConsultarPrestamos();

            // Pedimos DNI para registrar en el pr�stamo y validamos que exista.
            Console.WriteLine("Pantalla de Ingreso de Pr�stamos");
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
                        Utilidades.MensajeError("El cliente con DNI: {dni} no existe. \nPresione una tecla para intentar de nuevo.");
                        Console.ReadKey();
                        continue;
                    }
                    break;
                }
                break;
            }

            // Mostramos pel�culas disponibles y pedimos al usuario que ingrese el Id de la misma.   
            while (true)
            {
                Console.Clear();
                foreach (var pelicula in peliculasResponse.Data)
                {
                    Console.WriteLine($"Id Pelicula: {pelicula.IdPelicula} - Titulo: {pelicula.Titulo}");
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la pel�cula:");
                // Validamos el Id de Pelicula ingresado.
                foreach (var pelicula in peliculasResponse.Data)
                {
                    if (pelicula.IdPelicula.Equals(idPelicula))
                    {
                        nombrePelicula = pelicula.Titulo;
                        Utilidades.MensajeExito($"\nSeleccion� la pel�cula: {pelicula.Titulo} con Id: {pelicula.IdPelicula}");
                        int opc = Utilidades.PedirMenu("1. Continuar 2. Eligir nueva pel�cula", 1, 2);
                        switch(opc)
                        {
                            case 1: break; 
                            case 2: continue;
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError("No se encontr� el Id de Pel�cula ingresado. \nPresione una tecla para ingresar nueva pel�cula.");
                        Console.ReadKey();
                        continue;
                    }
                }
                // Validamos que la pel�cula tenga copias
                foreach (var copia in copiasResponse.Data)
                {
                    if (copia.IdPelicula.Equals(idPelicula))
                    {
                        idCopia = copia.IdCopia;
                        copia.CopiasDisponibles --;
                    }
                    else
                    {
                        Utilidades.MensajeError("Lo sentimos! La pel�cula seleccionada no posee copias disponibles. \nPresione una tecla para elegir nuevamente.");
                        Console.ReadKey();
                        continue;
                    }
                    break;
                }
                break;
            }

            // En este punto ya contamos con: IdCliente - IdPelicula - IdCopia

            // Buscamos ID mas alto de pr�stamos
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

            // Validamos pr�stamo previo a su ingreso
            Console.WriteLine("\nSe han ingresado los siguientes datos de pr�stamo:" +
                $"\nCliente: {nombreCliente} con DNI: {dni}" +
                $"\nPel�cula: {nombrePelicula} con Id: {idPelicula}" +
                $"\nPlazo: {plazo}" +
                $"\nFecha Tentativa de Devoluci�n: {fechaDevolucionTentativa}" +
                $"\nFecha Real de Devoluci�n:{fechaDevolucionReal}");
            int opcMenu = Utilidades.PedirMenu("1. Continuar 2. Abortar", 1, 2);
            switch (opcMenu)
            {
                case 1:
                    // Instanciamos nuevo prestamo
                    Prestamo nuevoPrestamo = new Prestamo(idPrestamo, idCliente, idCopia, plazo, fechaPrestamo, fechaDevolucionTentativa, fechaDevolucionReal);

                    //Agregamos prestamo e informamos si se realiz� correctamente o no 
                    var nuevoPrestamoResponse = prestamoDatos.AgregarPrestamo(nuevoPrestamo);
                    if (nuevoPrestamoResponse)
                    {
                        Console.Clear();
                        Utilidades.MensajeExito("\nPr�stamo agregado con �xito! \nPresione una tecla para continuar.");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Utilidades.MensajeError("\nIngreso de pr�stamo abortado! \nPresione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }
        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al agregar pr�stamo. Descripci�n del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }

    }
    internal static void VisualizarPrestamosCliente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Pr�stamos por Cliente\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();
            var clienteDatos = new ClienteNegocio();
            
            // Traemos prestamos y clientes
            var prestamoResponse = prestamoDatos.ConsultarPrestamos();
            var clienteResponse = clienteDatos.ConsultarClientes();

            /*
            var copiaResponse = copiaDatos.ConsultarCopias();
            var peliculaResponse = peliculaDatos.ConsultarPeliculas();
            */

            // Pedimos ID cliente y buscamos los prestamos asociados
            var IdCliente = Utilidades.PedirInt("Por favor ingrese el ID del cliente para ver sus pr�stamos:");
            foreach ( var prestamo in prestamoResponse.Data)
            {
                if(prestamo.IdCliente.Equals(IdCliente))
                {
                    foreach(var cliente in clienteResponse.Data)
                    {
                        if(cliente.Id.Equals(prestamo.IdCliente))
                        {
                            /*
                            foreach (var copia in copiaResponse.Data)
                            {
                                if (copia.IdCopia.Equals(prestamo.IdCopia))
                                {
                                    foreach (var pelicula in peliculaResponse.Data)
                                    {
                                        if (pelicula.IdPelicula.Equals(copia.IdPelicula))
                                        {
                            */
                                            Console.WriteLine($"El cliente {cliente.Nombre} {cliente.Apellido} solicit� un pr�stamo el d�a: {prestamo.FechaPrestamo} de la pelicula: 'pelicula.Titulo'\n");
                            /*
                                        }
                                    }
                                }
                            }
                            */
                            
                        }
                    }
                }
                else
                {
                    Utilidades.MensajeError("\nNo se encontraron pr�stamos asociados a ese ID.");
                }
            }
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();

        }
        catch (Exception ex) 
        {
            Utilidades.MensajeError($"\nError al consultar pr�stamo. Descripci�n del Error: {ex.Message} \nPresione una tecla para continuar.");
            Console.ReadKey();
        }                
    }
}