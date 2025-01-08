using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Pelicula_SalaCine.Request
{
    public class PeliculaSalaRequestDto
    {
        public int Id_sala_cine { get; set; }
        public int Id_pelicula { get; set; }
        public DateTime Fecha_publicacion { get; set; }
        public DateTime FechaFin { get; set; }
        public int State { get; set; }
    }
}
