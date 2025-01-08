using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Contexts.Configurations
{
    public class SalaCineConfiguration : IEntityTypeConfiguration<SalaCine>
    {
        public void Configure(EntityTypeBuilder<SalaCine> builder)
        {
            builder.HasKey(e => e.Id);
                   

            builder.ToTable("sala_cine");

            builder.Property(e => e.Id).HasColumnName("id_sala");

            builder.Property(e => e.AuditCreateDate)
                .HasColumnName("auditCreateDate")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.AuditCreateUser).HasColumnName("auditCreateUser");

            builder.Property(e => e.AuditDeleteDate).HasColumnName("auditDeleteDate");

            builder.Property(e => e.AuditDeleteUser).HasColumnName("auditDeleteUser");

            builder.Property(e => e.AuditUpdateDate).HasColumnName("auditUpdateDate");

            builder.Property(e => e.AuditUpdateUser).HasColumnName("auditUpdateUser");

            builder.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");

            builder.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");
/*
            builder.HasData(
            new SalaCine { Id = 1, Nombre = "Sala 1", Estado = "Disponible", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
            new SalaCine { Id = 2, Nombre = "Sala 2", Estado = "Disponible", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
            new SalaCine { Id = 3, Nombre = "Sala 3", Estado = "Disponible", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
            new SalaCine { Id = 4, Nombre = "Sala 4", Estado = "Disponible", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
            new SalaCine { Id = 5, Nombre = "Sala 5", Estado = "Disponible", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
            new SalaCine { Id = 6, Nombre = "Sala 6", Estado = "Disponible", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 },
            new SalaCine { Id = 7, Nombre = "Sala 7", Estado = "Disponible", AuditCreateUser = 1, AuditCreateDate = DateTime.Now, State = 1 }
        );*/
        }
    }
}
