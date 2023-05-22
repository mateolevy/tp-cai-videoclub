using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Entidades
{
    internal class Copia
    {
        private int _idCopia;
        private int _idPelicula;
        private DateTime _fechaAlta;
        private string _observaciones;
        private decimal _precio;

        public Copia(int idCopia, int idPelicula, DateTime fechaAlta, string observaciones, decimal precio)
        {
            _idCopia = idCopia;
            _idPelicula = idPelicula;
            _fechaAlta = fechaAlta;
            _observaciones = observaciones;
            _precio = precio;
        }

        public int IdCopia { get => _idCopia; set => _idCopia = value; }
        public int IdPelicula { get => _idPelicula; set => _idPelicula = value; }
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
        public decimal Precio { get => _precio; set => _precio = value; }
    }
}
