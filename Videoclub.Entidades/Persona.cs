using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Entidades
{
    public abstract class Persona
    {
        protected int _dni;
        protected string _apellido;
        protected string _nombre;
        protected DateTime _fechaNac;

        public Persona(int dni, string apellido, string nombre, DateTime fechaNac)
        {
            _dni = dni;
            _apellido = apellido;
            _nombre = nombre;
            _fechaNac = fechaNac;
        }

        public int Dni { get => _dni; set => _dni = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public DateTime FechaNac { get => _fechaNac; set => _fechaNac = value; }
    }
}
