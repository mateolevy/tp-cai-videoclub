using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.AccesoDatos
{
    public class CopiaDatos
    {
        public RestResponse<List<Copia>> ConsultarCopias()
        {
            var copiasResponse = RestClient.GetAsync<List<Copia>>("VideoClub/Copia").Result;
            return copiasResponse;
        }
        
        public RestResponse<List<Copia>> ConsultarCopiasPorIdPelicula(int idPelicula)
        {
            var copiasResponse = RestClient.GetAsync<List<Copia>>($"VideoClub/Copia/{idPelicula}").Result;
            return copiasResponse;
        }
    }
}
