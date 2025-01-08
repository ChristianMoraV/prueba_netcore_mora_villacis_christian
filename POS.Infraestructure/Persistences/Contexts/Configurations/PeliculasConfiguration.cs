using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Contexts.Configurations
{
    public class PeliculasConfiguration : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("pelicula");

            builder.Property(e => e.Id).HasColumnName("id_pelicula");

            builder.Property(e => e.AuditCreateDate)
                .HasColumnName("auditCreateDate")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.AuditCreateUser).HasColumnName("auditCreateUser");

            builder.Property(e => e.AuditDeleteDate).HasColumnName("auditDeleteDate");

            builder.Property(e => e.AuditDeleteUser).HasColumnName("auditDeleteUser");

            builder.Property(e => e.AuditUpdateDate).HasColumnName("auditUpdateDate");

            builder.Property(e => e.AuditUpdateUser).HasColumnName("auditUpdateUser");

            builder.Property(e => e.Duracion).HasColumnName("duracion");

            builder.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");
        }
    }
}
