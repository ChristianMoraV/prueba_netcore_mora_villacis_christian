using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Contexts.Configurations
{
    public class PeliculasSalasConfiguration : IEntityTypeConfiguration<PeliculaSala>
    {
        public void Configure(EntityTypeBuilder<PeliculaSala> builder)
        {
            builder.HasKey(e => e.Id);
                   

            builder.ToTable("pelicula_sala");

            builder.Property(e => e.Id).HasColumnName("id_pelicula_sala");

            builder.Property(e => e.AuditCreateDate)
                .HasColumnName("auditCreateDate")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.AuditCreateUser).HasColumnName("auditCreateUser");

            builder.Property(e => e.AuditDeleteDate).HasColumnName("auditDeleteDate");

            builder.Property(e => e.AuditDeleteUser).HasColumnName("auditDeleteUser");

            builder.Property(e => e.AuditUpdateDate).HasColumnName("auditUpdateDate");

            builder.Property(e => e.AuditUpdateUser).HasColumnName("auditUpdateUser");

            builder.Property(e => e.FechaFin).HasColumnName("fecha_fin");

            builder.Property(e => e.FechaPublicacion).HasColumnName("fecha_publicacion");

            builder.Property(e => e.IdPelicula).HasColumnName("id_pelicula");

            builder.Property(e => e.IdSalaCine).HasColumnName("id_sala_cine");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdPeliculaNavigation)
                .WithMany(p => p.PeliculaSalas)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pelicula___id_pe__4222D4EF");

            builder.HasOne(d => d.IdSalaCineNavigation)
                .WithMany(p => p.PeliculaSalas)
                .HasForeignKey(d => d.IdSalaCine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pelicula___id_sa__412EB0B6");
        }
    }
}
