using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.AccesoDatos
{
    public class PeliculaDatos
    {
        public RestResponse<List<Pelicula>> ConsultarPeliculas()
        {
            var peliculasResponse = RestClient.GetAsync<List<Pelicula>>("VideoClub/Pelicula").Result;
            return peliculasResponse; 
        }
        
        public RestResponse<Pelicula> ConsultarPeliculaPorId(int idPelicula)
        {
            var peliculaResponse = RestClient.GetAsync<Pelicula>($"VideoClub/Pelicula/{idPelicula}").Result;
            return peliculaResponse;
        }

        public RestResponse<Pelicula> AltaPelicula(Pelicula nuevaPelicula)
        {
            var peliculasResponse = RestClient.PostAsync("VideoClub/Pelicula", nuevaPelicula).Result;
            return peliculasResponse; 
        }
    }
}
