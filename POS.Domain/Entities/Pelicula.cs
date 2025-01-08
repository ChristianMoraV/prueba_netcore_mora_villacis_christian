using System;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public partial class Pelicula: BaseEntity
    {
        public Pelicula()
        {
            PeliculaSalas = new HashSet<PeliculaSala>();
        }
        public string Nombre { get; set; } = null!;
        public int Duracion { get; set; }
       

        public virtual ICollection<PeliculaSala> PeliculaSalas { get; set; }
    }
}
