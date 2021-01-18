using Microsoft.EntityFrameworkCore;
using Nexos.Domain;
using Nexos.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nexos.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity: Entity
    {
        readonly NexosDbContext _nexosDbContext;
        public Repository(NexosDbContext context)
        {
            _nexosDbContext = context;
        }
        public abstract DbSet<TEntity> Dbset { get; }

        public virtual void Add(TEntity entity)
        {
            Dbset.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Dbset.Remove(entity);
        }

        public virtual TEntity Get(Guid id)
        {
            return Dbset.FirstOrDefault(e => e.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Dbset.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Dbset.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            Dbset.Update(entity);
        }

        public void SaveChanges()
        {
            _nexosDbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _nexosDbContext.SaveChangesAsync();
        }
    }
}
