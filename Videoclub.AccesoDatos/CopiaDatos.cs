using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.AccesoDatos
{
    public class CopiaDatos
    {
        public RestResponse<List<Copia>> ConsultarCopias()
        {
            var copiasResponse = RestClient.GetAsync<List<Copia>>("VideoClub/Copias").Result;
            return copiasResponse;
        }
    }
}
