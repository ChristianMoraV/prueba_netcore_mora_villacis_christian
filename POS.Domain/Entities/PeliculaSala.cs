using System;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public partial class PeliculaSala:BaseEntity
    {
        public int IdSalaCine { get; set; }
        public int IdPelicula { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaFin { get; set; }
 
        public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;
        public virtual SalaCine IdSalaCineNavigation { get; set; } = null!;
    }
}
