namespace Videoclub.Entidades.Base
{
    public abstract class Persona
    {
        private int _dni;
        private string _apellido;
        private string _nombre;
        private DateTime _fechaNac;

        protected Persona(int dni, string apellido, string nombre, DateTime fechaNac)
        {
            _dni = dni;
            _apellido = apellido;
            _nombre = nombre;
            _fechaNac = fechaNac;
        }

        // Agrego empty constructor base con atributo JsonConstructor para poder serializar/deserializar utilizando JSON.Net
        protected Persona()
        {
        }

        public int Dni { get => _dni; set => _dni = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string NombreCompleto => $"{Nombre} {Apellido}" ;
        public DateTime FechaNacimiento { get => _fechaNac; set => _fechaNac = value; }
    }
}
