using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.SalaCine.Response
{
    public class SalaCineResponseDto
    {
        public int Id_sala { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set;}
        public int State { get; set; }
        public string? StateCategory { get; set; }
        public DateTime? AuditCreateDate { get; set; }
    }
}
