using Microsoft.EntityFrameworkCore;
using Prueba.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Repositories
{
    public class Repository<TEntity, TKey>
  : IRepository<TEntity, TKey>
  where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbSet.FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);

        public void Update(TEntity entity)
            => _dbSet.Update(entity);

        public void Remove(TEntity entity)
            => _dbSet.Remove(entity);
    }

}
