namespace Videoclub.Entidades;

public class Cliente : Persona
{
    private int _idCliente;
    private DateTime _fechaAlta;
    private string _direccion;
    private string _email;
    private string _telefono;

    public Cliente(int dni, string apellido, string nombre, DateTime fechaNac, int idCliente, DateTime fechaAlta, string direccion, string email, string telefono) : base(dni, apellido, nombre, fechaNac)
    {
        _idCliente = idCliente;
        _fechaAlta = fechaAlta;
        _direccion = direccion;
        _email = email;
        _telefono = telefono;
    }

    public int IdCliente { get => _idCliente; set => _idCliente = value; }
    public DateTime FechaAlta { set => _fechaAlta = value; }
    public int Antiguedad => (DateTime.Today - _fechaAlta).Days / 365 ;
    public string Direccion { get => _direccion; set => _direccion = value; }
    public string Email { get => _email; set => _email = value; }
    public string Telefono { get => _telefono; set => _telefono = value; }
}