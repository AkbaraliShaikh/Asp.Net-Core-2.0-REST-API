using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore.Api.Services
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter);
        Task CreateAsync(TEntity entity);
        Task CreateAsync(IList<TEntity> entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
