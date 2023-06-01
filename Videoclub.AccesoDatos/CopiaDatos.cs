using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videoclub.AccesoDatos.Utilidades;
using Videoclub.Entidades;

namespace Videoclub.AccesoDatos
{
    public class CopiaDatos
    {
        public RestResponse<List<Copia>> ConsultarCopias()
        {
            // Traer clientes unicamente que tengan usuario (registro) nro 854851
            var copiasResponse = RestClient.GetAsync<List<Copia>>("VideoClub/Copias").Result;
            return copiasResponse;
        }
    }
}
