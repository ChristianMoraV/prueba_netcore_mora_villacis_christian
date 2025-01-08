using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Pelicula.Request
{
    public class PeliculasRequestDto
    {
        public string? Nombre { get; set; }
        public int Duracion { get; set; }
        public int State { get; set; }
    }
}
