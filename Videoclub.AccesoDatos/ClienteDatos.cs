using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.AccesoDatos;

public class ClienteDatos
{
    public RestResponse<List<Cliente>> ConsultarClientes()
    {
        var clientesResponse = RestClient.GetAsync<List<Cliente>>("cliente").Result;

        return clientesResponse;
    }
    
    public Cliente AltaCliente()
    {
        throw new NotImplementedException();
    }
    
    public Cliente ConsultarClientePorUsuario(string usuario)
    {
        throw new NotImplementedException();
    }
}