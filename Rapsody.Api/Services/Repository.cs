using Rapsody.Api.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rapsody.Api.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RapsodyDbContext _rapsodyDbContext;

        public Repository(RapsodyDbContext rapsodyDbContext)
        {
            _rapsodyDbContext = rapsodyDbContext;
        }

        public async Task<IList<T>> GetAsync()
        {
            return await _rapsodyDbContext.Set<T>().ToListAsync();
        }

        public async Task SaveAsync(IList<T> entity)
        {
            await _rapsodyDbContext.Set<T>().AddRangeAsync(entity);
            await _rapsodyDbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(T entity)
        {
            await _rapsodyDbContext.Set<T>().AddAsync(entity);
            await _rapsodyDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _rapsodyDbContext.Set<T>().Update(entity);
            await _rapsodyDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _rapsodyDbContext.Set<T>().Remove(entity);
            await _rapsodyDbContext.SaveChangesAsync();
        }
    }
}
