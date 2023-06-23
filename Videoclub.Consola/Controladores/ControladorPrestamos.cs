using System.Globalization;
using Videoclub.Negocio;
using Videoclub.Entidades;
using Videoclub.AccesoDatos;

namespace Videoclub.Consola.Controladores;

internal class ControladorPrestamos
{
    internal static void ConsultarPrestamosPorPelicula()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Consulta de Préstamos por Película\n");

        try
        {
            var prestamoDatos = new PrestamoNegocio();
            var peliculaDatos = new PeliculaNegocio();
            var copiasDatos = new CopiaNegocio();

            int idPelicula;

            while (true)
            {
                var peliculasResponse = peliculaDatos.ConsultarPeliculas();
                
                if (peliculasResponse.Success && peliculasResponse.Data.Any())
                {
                    PrintTablaPeliculas(peliculasResponse.Data);
                }
                else
                {
                    Utilidades.MensajeError("No existen películas registradas.");
                    break;
                }

                idPelicula = Utilidades.PedirInt("\nIngrese el Id de la Película:");

                // Validamos el Id de Pelicula ingresado.
                var peliculaResponse = peliculaDatos.ConsultarPeliculaPorId(idPelicula);
                if (peliculaResponse.Success)
                {
                    var pelicula = peliculaResponse.Data;
                    Utilidades.MensajeExito($"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.Id}");
                    int opc = Utilidades.PedirMenu("1. Continuar. \n2. Eligir Nueva Película.", 1, 2);
                    if (opc == 1)
                    {
                        Console.Clear();
                        Console.WriteLine($"Consulta de Préstamos para la Película: {pelicula.Titulo}:\n");

                        var copiasDePeliculaElegida = copiasDatos.ConsultarCopiasPorIdPelicula(pelicula.Id);

                        if (copiasDePeliculaElegida.Success && copiasDePeliculaElegida.Data.Any())
                        {
                            var copias = copiasDePeliculaElegida.Data;
                            var prestamos = prestamoDatos.ConsultarPrestamos();

                            if (prestamos.Success && prestamos.Data.Any())
                            {
                                PrintTablaPrestamosPorCliente(prestamos.Data, copias, pelicula);
                            }
                            else
                            {
                                Utilidades.MensajeError(
                                    $"No se encontraron préstamos para la película: {pelicula.Titulo}");
                            }
                        }
                        else
                        {
                            Utilidades.MensajeError("No existen copias registradas para la película elegida.");
                        }
                    }
                    else if (opc == 2)
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Utilidades.MensajeError(
                        $"No se encontró el Id de película: {idPelicula}.");
                }
                break;
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError(
                $"\nError al consultar préstamo. \nDescripción del Error: {ex.Message} \nPresione una tecla para continuar.");
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }

    internal static void IngresarNuevoPrestamo()
    {
        Console.Clear();

        try
        {
            var prestamoDatos = new PrestamoNegocio();

            DateTime fechaPrestamo;
            int idCliente = 0;
            int idCopia = 0;
            int idPelicula = 0;
            int dni;
            string nombreCliente = "";
            string nombrePelicula = "";
            bool volverAlMenuPrincipal = false;

            var peliculaDatos = new PeliculaNegocio();
            var copiaDatos = new CopiaNegocio();
            var clienteDatos = new ClienteNegocio();

            // Traemos datos de las pelculas / copias / clientes.
            var peliculasResponse = peliculaDatos.ConsultarPeliculas();
            var clientesResponse = clienteDatos.ConsultarClientes();

            // Pedimos DNI para registrar en el prestamo y validamos que exista.
            Console.WriteLine("Pantalla de Ingreso de Préstamos");
            while (true)
            {
                dni = Utilidades.PedirDni("\nIngrese DNI del Cliente:");
                var clientePorDni = clientesResponse.Data.FirstOrDefault(cliente => cliente.Dni.Equals(dni));
                if (clientePorDni != null)
                {
                    idCliente = clientePorDni.Id;
                    nombreCliente = clientePorDni.NombreCompleto;
                }
                else
                {
                    Console.Clear();
                    Utilidades.MensajeError($"El cliente con DNI: {dni} no existe.");
                    int opcSeguir = Utilidades.PedirMenu("1. Ingresar otro DNI. \n2. Volver al Menú Principal.", 1, 2);
                    switch (opcSeguir)
                    {
                        case 1:
                            continue;
                        case 2:
                            volverAlMenuPrincipal = true;
                            break;
                    }
                }
                break;
            }

            if (peliculasResponse.Success && peliculasResponse.Data.Any())
            {
                // Mostramos pelculas disponibles y pedimos al usuario que ingrese el Id de la misma.
                if (volverAlMenuPrincipal == false)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Películas Disponibles:\n");

                        PrintTablaPeliculas(peliculasResponse.Data);

                        idPelicula = Utilidades.PedirInt("\nIngrese el Id de la Película:");
                        var peliculaPorId = peliculaDatos.ConsultarPeliculaPorId(idPelicula);

                        // Validamos el Id de Pelicula ingresado.
                        if (peliculaPorId.Success)
                        {
                            var pelicula = peliculaPorId.Data;
                            nombrePelicula = pelicula.Titulo;
                            Utilidades.MensajeExito(
                                $"\nSeleccionó la película: {pelicula.Titulo} con Id: {pelicula.Id}");
                            int opc = Utilidades.PedirMenu("1. Continuar. \n2. Eligir nueva película.", 1, 2);
                            switch (opc)
                            {
                                case 1: break;
                                case 2: continue;
                            }
                        }
                        else
                        {
                            Utilidades.MensajeError("\nNo se encontró el Id de película ingresada.");
                            int opcSeguir = Utilidades.PedirMenu("1. Ingresar otra película. \n2. Volver al Menú Principal.", 1, 2);
                            switch (opcSeguir)
                            {
                                case 1:
                                    continue;
                                case 2:
                                    volverAlMenuPrincipal = true;
                                    break;
                            }
                        }

                        if (volverAlMenuPrincipal == false)
                        {
                            // Validamos que la pelcula tenga copias
                            var copiasPorPeliculaId = copiaDatos.ConsultarCopiasPorIdPelicula(idPelicula);
                            if (copiasPorPeliculaId.Success && copiasPorPeliculaId.Data.Any())
                            {
                                idCopia = copiasPorPeliculaId.Data.First().Id;
                            }
                            else
                            {
                                Console.Clear();
                                Utilidades.MensajeError("La película seleccionada no posee copias disponibles.");
                                int opcSeguir =
                                    Utilidades.PedirMenu("1. Elegir otra Película. \n2. Volver al Menú Principal.", 1, 2);
                                switch (opcSeguir)
                                {
                                    case 1:
                                        continue;
                                    case 2:
                                        volverAlMenuPrincipal = true;
                                        break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                Utilidades.MensajeError(
                    $"No se encontraron películas registradas por lo que no es posible realizar un préstamo. \nPresione una tecla para volver al menú principal.");
                Console.ReadKey();
                volverAlMenuPrincipal = true;
            }

            if (volverAlMenuPrincipal == false)
            {
                // En este punto ya contamos con: IdCliente - IdPelicula - IdCopia

                Console.Clear();

                //Datos de entrada para el nuevo prestamo
                int plazo = Utilidades.PedirPlazo("Ingrese el Plazo (en días)");
                fechaPrestamo = DateTime.Now;
                DateTime fechaDevolucionTentativa = fechaPrestamo.AddDays(plazo);

                // Validamos prestamo previo a su ingreso
                Console.Clear();
                Console.WriteLine("\nSe han ingresado los siguientes datos de préstamo:" +
                                  $"\nCliente: {nombreCliente} con DNI: {dni}" +
                                  $"\nPelícula: {nombrePelicula} con Id: {idPelicula}" +
                                  $"\nPlazo: {plazo}" +
                                  $"\nFecha Tentativa de Devolución: {fechaDevolucionTentativa.ToString(CultureInfo.InvariantCulture)}");
                int opcMenu = Utilidades.PedirMenu("1. Continuar. \n2. Abortar.", 1, 2);
                switch (opcMenu)
                {
                    case 1:
                        // Instanciamos nuevo prestamo
                        Prestamo nuevoPrestamo = new Prestamo(0, idCliente, idCopia, plazo, fechaPrestamo,
                            fechaDevolucionTentativa);

                        //Agregamos prestamo e informamos si se realiz correctamente o no 
                        var nuevoPrestamoResponse = prestamoDatos.AgregarPrestamo(nuevoPrestamo);
                        if (nuevoPrestamoResponse)
                        {
                            Console.Clear();
                            Utilidades.MensajeExito("\nPréstamo agregado con éxito.");
                        }

                        break;
                    case 2:
                        Console.Clear();
                        Utilidades.MensajeError("\nIngreso de préstamo abortado.");
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            Utilidades.MensajeError(
                $"\nError al agregar préstamo. Descripción del Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
    
    internal static void VisualizarReportePrestamosPorCliente()
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Reporte de Préstamos por Cliente\n");

        try
        {
            var prestamoNegocio = new PrestamoNegocio();
            var clienteNegocio = new ClienteNegocio();
            var peliculaNegocio = new PeliculaNegocio();
            var copiasNegocio = new CopiaNegocio();

            // Verificamos que haya préstamos
            if (prestamoNegocio.ConsultarPrestamos().Data.Any())
            {
                // Traemos clientes y los mostramos en pantalla
                var clientesResponse = clienteNegocio.ConsultarClientes();
                Console.WriteLine("Listado de Clientes:\n");

                PrintTablaClientes(clientesResponse.Data);

                int idCliente = 0;
                string? nombreCliente = null;
                bool volverAlMenuPrincipal = false;

                // Pedimos DNI del cliente y buscamos su Id Cliente para luego buscar Prestamos.
                while (true)
                {
                    var dni = Utilidades.PedirDni("\nIngrese DNI del cliente:");

                    var clienteExistente = clientesResponse.Data.FirstOrDefault(cliente => cliente.Dni.Equals(dni));
                    if (clienteExistente != null)
                    {
                        idCliente = clienteExistente.Id;
                        nombreCliente = clienteExistente.NombreCompleto;
                    }
                    else
                    {
                        Console.Clear();
                        Utilidades.MensajeError($"El cliente con DNI: {dni} no existe.");
                        int opcSeguir =
                            Utilidades.PedirMenu("1. Ingresar otro DNI. \n2. Volver al Menú Principal.", 1, 2);
                        switch (opcSeguir)
                        {
                            case 1:
                                continue;
                            case 2:
                                volverAlMenuPrincipal = true;
                                break;
                        }
                    }

                    if (volverAlMenuPrincipal == false)
                    {
                        Console.Clear();
                        // Buscamos prestamos asociados al Id Cliente
                        var prestamosResponse = prestamoNegocio.ConsultarPrestamos();

                        var prestamosExistentes = prestamosResponse.Data
                            .Where(cliente => cliente.IdCliente.Equals(idCliente)).ToList();

                        if (prestamosExistentes.Any())
                        {
                            Console.WriteLine(
                                $"\nReporte de préstamos del cliente: {nombreCliente}. \nTotal de préstamos encontrados: {prestamosExistentes.Count}\n");

                            var copiasResponse = copiasNegocio.ConsultarCopias();

                            // Header de la tabla
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("{0, -20} | {1, -30} | {2, -10} | {3, -15} | {4, -8}", "Fecha Préstamo",
                                "Fecha Devolución Tentativa", "Plazo", "Título Película", "Id Copia");
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (var prestamo in prestamosExistentes)
                            {
                                var copia = copiasResponse.Data.FirstOrDefault(copia =>
                                    copia.Id.Equals(prestamo.IdCopia));
                                if (copia == null) continue;

                                var pelicula = peliculaNegocio.ConsultarPeliculaPorId(copia.IdPelicula);
                                Console.WriteLine("{0, -20} | {1, -30} | {2, -10} | {3, -15} | {4, -8}",
                                    prestamo.FechaPrestamo, prestamo.FechaPrestamo.Date, $"{prestamo.Plazo} días",
                                    pelicula.Data.Titulo, copia.Id);
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Utilidades.MensajeError("\nNo se encontraron préstamos asociados a ese cliente.");
                            int opcSeguir = Utilidades.PedirMenu("1. Ingresar otro DNI. \n2. Volver al Menú Principal.",
                                1, 2);
                            switch (opcSeguir)
                            {
                                case 1:
                                    continue;
                                case 2:
                                    break;
                            }
                        }
                    }

                    break;
                }
            }
            else
            {
                Utilidades.MensajeError("No existen préstamos registrados.");
            }
        }
        catch (Exception ex)
        {
            Utilidades.MensajeError(
                $"\nError al consultar reporte de préstamos por cliente. \nDescripción del Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar.");
            Console.ReadKey();
        }
    }
    
    private static void PrintTablaPeliculas(List<Pelicula> peliculas)
    {
        // Header de la tabla
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -15}", "Id Película", "Título");
        Console.ForegroundColor = ConsoleColor.White;

        foreach (var pelicula in peliculas)
        {
            Console.WriteLine("{0, -15} | {1, -15}", pelicula.Id, pelicula.Titulo);
        }
    }
    
    private static void PrintTablaClientes(List<Cliente> clientes)
    {
        // Header de la tabla
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -15} | {2, -15}", "Nombre", "Apellido", "DNI");
        Console.ForegroundColor = ConsoleColor.White;

        foreach (var cliente in clientes)
        {
            Console.WriteLine("{0, -15} | {1, -15} | {2, -15}", cliente.Nombre, cliente.Apellido, cliente.Dni);
        }
    }
    
    private static void PrintTablaPrestamosPorCliente(List<Prestamo> prestamos, List<Copia> copias, Pelicula pelicula)
    {
        var clienteDatos = new ClienteNegocio();

        // Header de la tabla
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0, -15} | {1, -20} | {2, -15} | {3, -30}", "Id Préstamo",
            "Fecha Préstamo", "Película", "Cliente");
        Console.ForegroundColor = ConsoleColor.White;
        
        foreach (var copia in copias)
        {
            var prestamosPorIdCopia = prestamos.Where(prestamo => prestamo.IdCopia.Equals(copia.Id)).ToList();
            foreach (var prestamo in prestamosPorIdCopia)
            {
                var clienteDelPrestamo = clienteDatos.ConsultarClientes().Data
                    .FirstOrDefault(cliente => cliente.Id.Equals(prestamo.IdCliente));
                if (clienteDelPrestamo != null)
                {
                    Console.WriteLine("{0, -15} | {1, -20} | {2, -15} | {3, -30}",
                        prestamo.Id, prestamo.FechaPrestamo, pelicula.Titulo,
                        clienteDelPrestamo.NombreCompleto);
                }
            }
        }
    }
}