using System.Text.Json.Serialization;

namespace Videoclub.Entidades
{
    public class Pelicula
    {
        private int _idPelicula;
        private int _anio;
        private int _duracion;
        private string _titulo;
        private string _prductora;
        private string _director;
        private string _genero;

        // Agrego empty constructor base con atributo JsonConstructor para poder serializar/deserializar utilizando JSON.Net
        [JsonConstructor]
        public Pelicula()
        {
        }
        
        public Pelicula(int idPelicula, int anio, string titulo, string productora, string genero, string director, int duracion)
        {
            _idPelicula = idPelicula;
            _anio = anio;
            _titulo = titulo;
            _prductora = productora;
            _genero = genero;
            _director = director;
            _duracion = duracion;
        }

        public int IdPelicula { get => _idPelicula; set => _idPelicula = value; }
        public int Anio { get => _anio; set => _anio = value; }
        public string Titulo { get => _titulo; set => _titulo = value; }
        public string Productora { get => _prductora; set => _prductora = value; }
        public string Genero { get => _genero; set => _genero = value; }
        public string Director { get => _director; set => _director = value; }
        public int Duracion { get => _duracion; set => _duracion = value; }
    }
}
