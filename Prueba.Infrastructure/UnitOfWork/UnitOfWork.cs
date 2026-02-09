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
    }

}
