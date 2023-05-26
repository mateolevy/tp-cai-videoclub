using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videoclub.Negocio.Excepciones
{
    internal class ObjetoNull : Exception
    {
        public ObjetoNull(string tipoObjeto):base($"El objeto: {tipoObjeto} ingresado es inválido.") { }
    }
}
