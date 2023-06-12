using System.Text.Json.Serialization;
using Videoclub.Entidades.Base;

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

    // Agrego empty constructor base con atributo JsonConstructor para poder serializar/deserializar utilizando JSON.Net
    [JsonConstructor]
    public Cliente()
    {
    }
    
    public Cliente(int idCliente, int dni, string apellido, string nombre, DateTime fechaNac, string direccion, string email, string telefono) : base(dni, apellido, nombre, fechaNac)
    {
        _idCliente = idCliente;
        _fechaAlta = DateTime.Now;
        _direccion = direccion;
        _email = email;
        _telefono = telefono;
        // utilizar siempre este nro. de registro para filtrar la consulta de GET
        _usuario = "854851";
        _activo = true;
    }
    
    public int Id { get => _idCliente; set => _idCliente = value; }
    public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
    public int Antiguedad => (DateTime.Today - _fechaAlta).Days / 365 ;
    public string Direccion { get => _direccion; set => _direccion = value; }
    public string Email { get => _email; set => _email = value; }
    public string Telefono { get => _telefono; set => _telefono = value; }
    public bool Activo { get => _activo; set => _activo = value; }
    public string Usuario { get => _usuario; set => _usuario = value; }
}