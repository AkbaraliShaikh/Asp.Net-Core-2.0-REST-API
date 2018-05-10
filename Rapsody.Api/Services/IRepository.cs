using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rapsody.Api.Services
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAsync();
        Task SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveAsync(IList<T> entity);
        Task DeleteAsync(T entity);
    }
}
