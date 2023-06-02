using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.AccesoDatos;

public class ClienteDatos
{
    public RestResponse<List<Cliente>> ConsultarClientes()
    {
        // Traer clientes unicamente que tengan usuario (registro) nro 854851
        var clientesResponse = RestClient.GetAsync<List<Cliente>>("cliente/854851").Result;
        return clientesResponse;
    }
    
    public RestResponse<Cliente> ConsultarClientePorTelefono(string telefono)
    {
        // Traer clientes unicamente que tengan usuario (registro) nro 854851
        var clientesResponse = RestClient.GetAsync<Cliente>($"cliente/{telefono}/telefono").Result;
        return clientesResponse;
    }
    
    public RestResponse<Cliente> AltaCliente(Cliente nuevoCliente)
    {
        var clientesResponse = RestClient.PostAsync("cliente", nuevoCliente).Result;
        return clientesResponse;
    }
}