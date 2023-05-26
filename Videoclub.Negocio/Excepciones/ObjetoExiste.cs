using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Negocio.Excepciones
{
    internal class ObjetoExiste : Exception
    {
        public ObjetoExiste(string tipoObjeto, string tipoIdentificador, int nroIdentificador) : base ($"El {tipoObjeto} con {tipoIdentificador}: {nroIdentificador} ya existe."){ }
    }
}
