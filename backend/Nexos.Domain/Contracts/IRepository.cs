using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nexos.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(Guid id);
        Task<TEntity> GetAsync(Guid id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
