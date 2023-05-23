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

    public RestResponse<Cliente> AgregarCliente(Cliente nuevoCliente)
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
        return nuevoclienteResponse;
    }

    public RestResponse<List<Cliente>> ConsultarClientes()
    {
        var clientesResponse = _clienteDatos.ConsultarClientes();
        return clientesResponse;
    }
}