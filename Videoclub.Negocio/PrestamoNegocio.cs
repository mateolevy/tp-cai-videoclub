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
    public class PrestamoNegocio
    {

        private PrestamoDatos _prestamoDatos;

        public PrestamoNegocio()
        {
            _prestamoDatos = new PrestamoDatos();
        }

        public bool AgregarPrestamo(Prestamo nuevoPrestamo)
        {
            // Traemos lista de prestamos
            var prestamosResponse = _prestamoDatos.ConsultarPrestamos();

            // Verificamos si prestamo null
            if(nuevoPrestamo is null)
            {
                throw new ObjetoNull("prestamo");
            }

            // Verificamos que el prestamo a agregar no exista
            if (prestamosResponse.Success)
            {
                if (prestamosResponse.Data.Any( p => p.IdPrestamo == nuevoPrestamo.IdPrestamo )) 
                {
                    throw new ObjetoExiste("Prestamo", "Id prestamo", nuevoPrestamo.IdPrestamo);
                }
            }

            // Agregamos prestamo y enviamos true a la consola
            var nuevoPrestamoResponse = _prestamoDatos.AltaPrestamo(nuevoPrestamo);
            if(nuevoPrestamoResponse.Success)
            {
                return true;
            }

            throw new TransactionError(nuevoPrestamoResponse.Error);
        }


        public RestResponse<List<Prestamo>> ConsultarPrestamos()
        {
            var prestamosResponse = _prestamoDatos.ConsultarPrestamos();                      
            return prestamosResponse;                       
        }
    }
}
