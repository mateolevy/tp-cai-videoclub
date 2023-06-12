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

        public bool AltaCopia(Copia nuevaCopia)
        {
            // Traemos copias
            var copiasResponse = _copiaDatos.ConsultarCopias();

            //Verificamos que la copia a ingresar no sea null 
            if (nuevaCopia is null)
            {
                throw new ObjetoNull("Copia");
            }

            // Verificamos si la copia a ingresar ya existe 
            if(copiasResponse.Success)
            {
                if(copiasResponse.Data.Any( c => c.Id == nuevaCopia.Id))
                {
                    throw new ObjetoExiste("Copia", "ID", nuevaCopia.Id);
                }
            }

            // Agregamos copia y pasamos true a la consola 
            var nuevaCopiaResponse = _copiaDatos.AltaCopia(nuevaCopia);
            if(nuevaCopiaResponse.Success)
            {
                return true;
            }

            throw new TransactionError(nuevaCopiaResponse.Error);
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
