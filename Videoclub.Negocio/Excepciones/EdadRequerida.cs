using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Negocio.Excepciones
{
    internal class EdadRequerida : Exception
    {
        public EdadRequerida() : base("El cliente ingresado debe tener al menos 8 años."){ }
    }
}
