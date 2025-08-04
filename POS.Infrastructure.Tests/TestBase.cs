using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Utilities.Static;

namespace POS.Infrastructure.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected POSCineContext Context { get; private set; }

        protected TestBase()
        {
            var options = new DbContextOptionsBuilder<POSCineContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new POSCineContext(options);
            Context.Database.EnsureCreated();
        }

        protected void SeedTestData()
        {
            var peliculas = new List<Pelicula>
            {
                new Pelicula
                {
                    Id = 1,
                    Nombre = "Test Movie 1",
                    Duracion = 120,
                    State = (int)StateTypes.Active,
                    AuditCreateDate = DateTime.Now.AddDays(-10),
                    AuditCreateUser = 1
                },
                new Pelicula
                {
                    Id = 2,
                    Nombre = "Test Movie 2",
                    Duracion = 90,
                    State = (int)StateTypes.Active,
                    AuditCreateDate = DateTime.Now.AddDays(-5),
                    AuditCreateUser = 1
                },
                new Pelicula
                {
                    Id = 3,
                    Nombre = "Deleted Movie",
                    Duracion = 110,
                    State = (int)StateTypes.Inactive,
                    AuditCreateDate = DateTime.Now.AddDays(-15),
                    AuditCreateUser = 1,
                    AuditDeleteDate = DateTime.Now.AddDays(-1),
                    AuditDeleteUser = 1
                }
            };

            var salaCines = new List<SalaCine>
            {
                new SalaCine
                {
                    Id = 1,
                    Nombre = "Sala 1",
                    Estado = "Disponible",
                    State = (int)StateTypes.Active,
                    AuditCreateDate = DateTime.Now.AddDays(-10),
                    AuditCreateUser = 1
                },
                new SalaCine
                {
                    Id = 2,
                    Nombre = "Sala 2",
                    Estado = "Disponible",
                    State = (int)StateTypes.Active,
                    AuditCreateDate = DateTime.Now.AddDays(-8),
                    AuditCreateUser = 1
                }
            };

            var peliculaSalas = new List<PeliculaSala>
            {
                new PeliculaSala
                {
                    Id = 1,
                    IdPelicula = 1,
                    IdSalaCine = 1,
                    FechaPublicacion = DateTime.Now.AddDays(-5),
                    FechaFin = DateTime.Now.AddDays(5),
                    State = (int)StateTypes.Active,
                    AuditCreateDate = DateTime.Now.AddDays(-5),
                    AuditCreateUser = 1
                }
            };

            Context.Peliculas.AddRange(peliculas);
            Context.SalaCines.AddRange(salaCines);
            Context.PeliculaSalas.AddRange(peliculaSalas);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context?.Dispose();
            }
        }
    }
}