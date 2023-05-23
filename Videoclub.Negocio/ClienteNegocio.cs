using Videoclub.AccesoDatos;
using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.Negocio;

public class ClienteNegocio
{
    private ClienteDatos _clienteDatos;

    public ClienteNegocio()
    {
        _clienteDatos = new ClienteDatos();
    }
    
    public RestResponse<List<Cliente>> ConsultarClientes()
    {
        var clientesResponse = _clienteDatos.ConsultarClientes();
        return clientesResponse;
    }
}