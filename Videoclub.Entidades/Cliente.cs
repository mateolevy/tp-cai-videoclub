using System.Text.Json.Serialization;

namespace Videoclub.Entidades;

public class Cliente : Persona
{
    private int _idCliente;
    private DateTime _fechaAlta;
    private string _direccion;
    private string _email;
    private string _telefono;
    private bool _activo;
    private string _usuario;
    private string _host;

    // Agrego empty constructor base con atributo JsonConstructor para poder serializar/desserializar utilizando JSON.Net
    [JsonConstructor]
    public Cliente()
    {
    }
    
    public Cliente(int dni, string apellido, string nombre, DateTime fechaNac, int idCliente, DateTime fechaAlta, string direccion, string email, string telefono, string usuario, bool activo) : base(dni, apellido, nombre, fechaNac)
    {
        _idCliente = idCliente;
        _fechaAlta = fechaAlta;
        _direccion = direccion;
        _email = email;
        _telefono = telefono;
        _usuario = usuario;
        _activo = activo;
    }
    
    public int Id { get => _idCliente; set => _idCliente = value; }
    public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
    public int Antiguedad => (DateTime.Today - _fechaAlta).Days / 365 ;
    public string Direccion { get => _direccion; set => _direccion = value; }
    public string Email { get => _email; set => _email = value; }
    public string Telefono { get => _telefono; set => _telefono = value; }
    public bool Activo { get => _activo; set => _activo = value; }
    public string Usuario { get => _usuario; set => _usuario = value; }
    private string Host { get => _host; set => _host = value; }
}