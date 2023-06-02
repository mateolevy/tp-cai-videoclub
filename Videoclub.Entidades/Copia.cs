using System.Text.Json.Serialization;

namespace Videoclub.Entidades
{
    public class Copia
    {
        private int _idCopia;
        private int _idPelicula;
        private DateTime _fechaAlta;
        private string _observaciones;
        private decimal _precio;
        private int _copiasDisponibles;
        
        // Agrego empty constructor base con atributo JsonConstructor para poder serializar/deserializar utilizando JSON.Net
        [JsonConstructor]
        public Copia()
        {
        }
        
        public Copia(int idCopia, int idPelicula, DateTime fechaAlta, string observaciones, decimal precio, int copiasDisponibles)
        {
            _idCopia = idCopia;
            _idPelicula = idPelicula;
            _fechaAlta = fechaAlta;
            _observaciones = observaciones;
            _precio = precio;
            _copiasDisponibles = copiasDisponibles;
        }

        public int Id { get => _idCopia; set => _idCopia = value; }
        public int IdPelicula { get => _idPelicula; set => _idPelicula = value; }
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
        public decimal Precio { get => _precio; set => _precio = value; }
        public int CopiasDisponibles { get => _copiasDisponibles; set => _copiasDisponibles = value; }
    }
}
