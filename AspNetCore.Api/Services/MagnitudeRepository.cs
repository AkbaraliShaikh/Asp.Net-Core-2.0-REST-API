using AspNetCore.Api.DB;
using AspNetCore.Api.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Api.Services
{
    public class MagnitudeRepository : Repository<Magnitude>, IMagnitudeRepository
    {
        private readonly EFCoreDbContext _efCoreDbContext;

        public MagnitudeRepository(EFCoreDbContext efCoreDbContext) : base(efCoreDbContext)
        {
            _efCoreDbContext = efCoreDbContext;
        }

        public async Task<Magnitude> GetAsync(int id)
        {
            return await _efCoreDbContext.Magnitude.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task TruncateAsync(string tableName)
        {
            return _efCoreDbContext.Database.ExecuteSqlCommandAsync(string.Format("TRUNCATE TABLE public.\"{0}\"", tableName));
        }
    }
}
