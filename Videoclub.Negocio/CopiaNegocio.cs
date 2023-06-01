using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videoclub.AccesoDatos.Utilidades;
using Videoclub.AccesoDatos;
using Videoclub.Entidades;
using Videoclub.Negocio.Excepciones;

namespace Videoclub.Negocio
{
    public class CopiaNegocio
    {
        private CopiaDatos _copiaDatos;

        public CopiaNegocio()
        {
            _copiaDatos = new CopiaDatos();
        }

        public RestResponse<List<Copia>> ConsultarCopias()
        {
            var copiasResponse = _copiaDatos.ConsultarCopias();
            return copiasResponse;
        }
    }
}
