using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public RestResponse<Pelicula> AltaPelicula(Pelicula nuevaPelicula)
        {
            var peliculasResponse = RestClient.PostAsync("VideoClub/Pelicula", nuevaPelicula).Result;
            return peliculasResponse; 
        }
    }
}
