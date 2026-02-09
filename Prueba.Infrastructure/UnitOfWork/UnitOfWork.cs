using Microsoft.EntityFrameworkCore.Storage;
using Prueba.Application.Interfaces;
using Prueba.Infrastructure.Persistence;
using Prueba.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public IRepository<TEntity, TKey> Repository<TEntity, TKey>()
           where TEntity : class
        {
            var type = typeof(IRepository<TEntity, TKey>);

            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity, TKey>(_context);
            }

            return (IRepository<TEntity, TKey>)_repositories[type];
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

}
