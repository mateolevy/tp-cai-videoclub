using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Entidades
{
    internal class Prestamo
    {
        private int _idPrestamo;
        private int _idCliente;
        private int _idCopia;
        private DateTime _plazo;
        private DateTime _fechaPrestamo;
        private DateTime _fechaDevolucionTentativa;
        private DateTime _fechaDevolucionReal;

        public Prestamo(int idPrestamo, int idCliente, int idCopia, DateTime plazo, DateTime fechaPrestamo, DateTime fechaDevolucionTentativa, DateTime fechaDevolucionReal)
        {
            _idPrestamo = idPrestamo;
            _idCliente = idCliente;
            _idCopia = idCopia;
            _plazo = plazo;
            _fechaPrestamo = fechaPrestamo;
            _fechaDevolucionTentativa = fechaDevolucionTentativa;
            _fechaDevolucionReal = fechaDevolucionReal;
        }

        public int IdPrestamo { get => _idPrestamo; set => _idPrestamo = value; } 

        public int IdCliente { get => _idCliente;  set => _idCliente = value; } 
        public int IdCopia { get => _idCopia; set => _idCopia = value; } 
        public DateTime Plazo { get => _plazo; set => _plazo = value; } 
        public DateTime FechaPrestamo { get => _fechaPrestamo; set => _fechaPrestamo = value; }
        public DateTime FechaDevolucionTentativa { get => _fechaDevolucionTentativa; set => _fechaDevolucionTentativa = value; }
        public DateTime FechaDevolucionReal { get => _fechaDevolucionReal; set => _fechaDevolucionReal = value; }
    }
}
