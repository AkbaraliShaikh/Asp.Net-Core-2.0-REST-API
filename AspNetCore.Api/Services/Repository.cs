using AspNetCore.Api.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace AspNetCore.Api.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EFCoreDbContext _efCoreDbContext;

        public Repository(EFCoreDbContext efCoreDbContext)
        {
            _efCoreDbContext = efCoreDbContext;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _efCoreDbContext.Set<T>().ToListAsync();
        }

        public async Task CreateAsync(IList<T> entity)
        {
            await _efCoreDbContext.Set<T>().AddRangeAsync(entity);
            await _efCoreDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _efCoreDbContext.Set<T>().AddAsync(entity);
            await _efCoreDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _efCoreDbContext.Set<T>().Update(entity);
            await _efCoreDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _efCoreDbContext.Set<T>().Remove(entity);
            await _efCoreDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter)
        {
            return await _efCoreDbContext.Set<T>().AsNoTracking().Where(filter).ToListAsync();
        }
    }
}
