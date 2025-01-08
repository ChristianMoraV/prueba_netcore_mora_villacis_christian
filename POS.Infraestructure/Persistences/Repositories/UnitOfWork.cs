using Microsoft.EntityFrameworkCore.Storage;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System.Data;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSCineContext _context;

        public IGenericRepository<Pelicula> _pelicula = null!;
        public IGenericRepository<SalaCine> _salaCine = null!;
        public IGenericRepository<PeliculaSala> _peliculaSala = null!;

        public UnitOfWork(POSCineContext context)
        {
            _context = context;
        }
        public IGenericRepository<Pelicula> Pelicula => _pelicula ?? new GenericRepository<Pelicula>(_context);
        public IGenericRepository<SalaCine> SalaCine => _salaCine ?? new GenericRepository<SalaCine>(_context);
        public IGenericRepository<PeliculaSala> PeliculaSala => _peliculaSala ?? new GenericRepository<PeliculaSala>(_context);

        public IDbTransaction BeginTransaction()
        {
            var transaction = _context.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
