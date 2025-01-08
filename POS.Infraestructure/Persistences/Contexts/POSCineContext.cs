using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using System.Reflection;

namespace POS.Infraestructure.Persistences.Contexts
{
    public partial class POSCineContext : DbContext
    {
        public POSCineContext()
        {
        }

        public POSCineContext(DbContextOptions<POSCineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;
        public virtual DbSet<PeliculaSala> PeliculaSalas { get; set; } = null!;
        public virtual DbSet<SalaCine> SalaCines { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational.Collection", "ModerN_Spanish_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
