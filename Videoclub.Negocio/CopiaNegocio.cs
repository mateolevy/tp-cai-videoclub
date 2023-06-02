using Videoclub.AccesoDatos.Utilidades;
using Videoclub.AccesoDatos;
using Videoclub.Entidades;

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
        
        public RestResponse<List<Copia>> ConsultarCopiasPorIdPelicula(int idPelicula)
        {
            var copiasResponse = _copiaDatos.ConsultarCopiasPorIdPelicula(idPelicula);
            return copiasResponse;
        }
    }
}
