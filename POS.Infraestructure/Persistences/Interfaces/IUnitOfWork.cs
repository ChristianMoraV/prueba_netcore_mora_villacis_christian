using POS.Domain.Entities;
using System.Data;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Pelicula> Pelicula { get; }
        IGenericRepository<SalaCine> SalaCine {get ;}
        IGenericRepository<PeliculaSala> PeliculaSala { get; }
        void SaveChanges();
        Task SaveChangesAsync();
        IDbTransaction BeginTransaction();
    }
}
