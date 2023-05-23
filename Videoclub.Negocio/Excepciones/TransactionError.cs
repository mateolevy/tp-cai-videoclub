using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Negocio.Excepciones
{
    internal class TransactionError : Exception
    {
        public TransactionError(string error) : base("Se produjo el siguiente error: " + error + ".") { }
    }
}
