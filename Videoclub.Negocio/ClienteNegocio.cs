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
        var clientesResponse = _clienteDatos.ConsultarClientes();
        if (nuevoCliente is null)
        {
            throw new ObjetoNull("Cliente");
        }
        if (clientesResponse.Success)
        {
            foreach (var c in clientesResponse.Data)
            {
                if (c.Dni == nuevoCliente.Dni)
                {
                    throw new ObjetoExiste("Cliente", "DNI" ,nuevoCliente.Dni);
                }
            }
        }
        var nuevoclienteResponse = _clienteDatos.AltaCliente(nuevoCliente);
        if (nuevoclienteResponse.Success)
        {
            return true;
        }
        else
        {
            throw new TransactionError(nuevoclienteResponse.Error);
        }
    }

    public RestResponse<List<Cliente>> ConsultarClientes()
    {
        var clientesResponse = _clienteDatos.ConsultarClientes();
        return clientesResponse;
    }

    public bool ExistenClientesIngresados()
    {
        var clienteDatos = new ClienteNegocio();
        var clientesResponse = clienteDatos.ConsultarClientes();
        if (clientesResponse.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}