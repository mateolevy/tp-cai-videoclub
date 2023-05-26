using System.Collections.Generic;
using Videoclub.AccesoDatos;
using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;
using Videoclub.Negocio.Excepciones;

namespace Videoclub.Negocio;

public class ClienteNegocio
{
    private ClienteDatos _clienteDatos;

    public ClienteNegocio()
    {
        _clienteDatos = new ClienteDatos();
    }

    public bool AgregarCliente(Cliente nuevoCliente)
    {
        // Traemos lista de clientes.
        var clientesResponse = _clienteDatos.ConsultarClientes();

        // Verificamos cliente null.
        if (nuevoCliente is null)
        {
            throw new ObjetoNull("Cliente");
        }

        // Verificamos que el que se quiere agregar no exista.
        if (clientesResponse.Success)
        {
            if (clientesResponse.Data.Any(c => c.Dni == nuevoCliente.Dni))
            {
                throw new ObjetoExiste("Cliente", "DNI" ,nuevoCliente.Dni);
            }
        }
        
        // Agregamos cliente y si sale bien, se le pasa el valor true a la capa de consola.
        var nuevoclienteResponse = _clienteDatos.AltaCliente(nuevoCliente);
        if (nuevoclienteResponse.Success)
        {
            return true;
        }

        throw new TransactionError(nuevoclienteResponse.Error);
    }

    public RestResponse<List<Cliente>> ConsultarClientes()
    {
        var clientesResponse = _clienteDatos.ConsultarClientes();
        return clientesResponse;
    }
}