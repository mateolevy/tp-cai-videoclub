using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Entidades
{
    public class Pelicula
    {
        private int _idPelicula;
        private int _anio;
        private string _titulo;
        private string _prductora;
        private string _genero;

        public Pelicula(int idPelicula, int anio, string titulo, string prductora, string genero)
        {
            _idPelicula = idPelicula;
            _anio = anio;
            _titulo = titulo;
            _prductora = prductora;
            _genero = genero;
        }

        public int IdPelicula { get => _idPelicula; set => _idPelicula = value; }
        public int Anio { get => _anio; set => _anio = value; }
        public string Titulo { get => _titulo; set => _titulo = value; }
        public string Prductora { get => _prductora; set => _prductora = value; }
        public string Genero { get => _genero; set => _genero = value; }
    }
}
