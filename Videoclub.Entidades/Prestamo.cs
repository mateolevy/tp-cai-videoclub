using System.Text.Json.Serialization;

namespace Videoclub.Entidades
{
    public class Prestamo
    {
        private int _idPrestamo;
        private int _idCliente;
        private int _idCopia;
        private int _plazo;
        private bool _activo;
        private DateTime _fechaPrestamo;
        private DateTime _fechaDevolucionTentativa;
        private DateTime _fechaDevolucionReal;

        // Agrego empty constructor base con atributo JsonConstructor para poder serializar/deserializar utilizando JSON.Net
        [JsonConstructor]
        public Prestamo()
        {
        }

        public Prestamo(int idPrestamo, int idCliente, int idCopia, int plazo, DateTime fechaPrestamo, DateTime fechaDevolucionTentativa, DateTime fechaDevolucionReal)
        {
            _idPrestamo = idPrestamo;
            _idCliente = idCliente;
            _idCopia = idCopia;
            _plazo = plazo;
            _activo = true;
            _fechaPrestamo = fechaPrestamo;
            _fechaDevolucionTentativa = fechaDevolucionTentativa;
            _fechaDevolucionReal = fechaDevolucionReal;
        }

        public int Id { get => _idPrestamo; set => _idPrestamo = value; } 

        public int IdCliente { get => _idCliente;  set => _idCliente = value; } 
        public int IdCopia { get => _idCopia; set => _idCopia = value; } 
        public int Plazo { get => _plazo; set => _plazo = value; } 
        public bool Activo { get => _activo; set => _activo = value; }
        public DateTime FechaPrestamo { get => _fechaPrestamo; set => _fechaPrestamo = value; }
        public DateTime FechaDevolucionTentativa { get => _fechaDevolucionTentativa; set => _fechaDevolucionTentativa = value; }
        public DateTime FechaDevolucionReal { get => _fechaDevolucionReal; set => _fechaDevolucionReal = value; }
    }
}
