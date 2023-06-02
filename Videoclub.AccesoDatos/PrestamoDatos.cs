using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.AccesoDatos
{
    public class PrestamoDatos
    {
        public RestResponse<List<Prestamo>> ConsultarPrestamos()
        {
            var prestamosResponse = RestClient.GetAsync<List<Prestamo>>("VideoClub/Prestamos").Result;
            return prestamosResponse;
        }

        public RestResponse<Prestamo> AltaPrestamo(Prestamo nuevoPrestamo)
        {
            var prestamosResponse = RestClient.PostAsync("VideoClub/Prestamos", nuevoPrestamo).Result;
            return prestamosResponse;
        }
    }
}
