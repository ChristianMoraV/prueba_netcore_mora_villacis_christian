using System;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public partial class SalaCine:BaseEntity
    {
        public SalaCine()
        {
            PeliculaSalas = new HashSet<PeliculaSala>();
        }
        public string Nombre { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public virtual ICollection<PeliculaSala> PeliculaSalas { get; set; }
    }
}
