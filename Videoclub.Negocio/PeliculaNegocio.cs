using Videoclub.AccesoDatos;
using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;
using Videoclub.Negocio.Excepciones;

namespace Videoclub.Negocio;

public class PeliculaNegocio
{
    private readonly PeliculaDatos _peliculaDatos;

    public PeliculaNegocio()
    {
        _peliculaDatos = new PeliculaDatos();
    }
    
    public bool AltaPelicula(Pelicula nuevaPelicula)
    {
        // Traemos peliculas
        var peliculasResponse = _peliculaDatos.ConsultarPeliculas();

        // Verificamos pelicula null
        if(nuevaPelicula is null)
        {
            throw new ObjetoNull("Pelicula");
        }

        // Verificamos si la pelicula a ingresar ya existe
        if (peliculasResponse.Success)
        {
            if(peliculasResponse.Data.Any(p => p.IdPelicula == nuevaPelicula.IdPelicula))
            {
                throw new ObjetoExiste("pelicula", "ID", nuevaPelicula.IdPelicula);
            }
        }

        // Agregamos pelicula y pasamos true a consola si se realiza correctamente
        var nuevaPeliculaResponse = _peliculaDatos.AltaPelicula(nuevaPelicula);
        if(nuevaPeliculaResponse.Success)
        {
            return true;
        }

        throw new TransactionError(nuevaPeliculaResponse.Error);
    }

    public RestResponse<List<Pelicula>> ConsultarPeliculas()
    {
        var peliculaResponse = _peliculaDatos.ConsultarPeliculas();
        return peliculaResponse;
    }
    
    public RestResponse<Pelicula> ConsultarPeliculaPorId(int idPelicula)
    {
        var peliculaResponse = _peliculaDatos.ConsultarPeliculaPorId(idPelicula);
        return peliculaResponse;
    }
}

