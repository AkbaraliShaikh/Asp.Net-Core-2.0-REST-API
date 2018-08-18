using System.Threading.Tasks;
using AspNetCore.Api.DB;
using AspNetCore.Api.Models;
using AspNetCore.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Api.Services
{
    public class CurrencyCodeRepository : Repository<CurrencyCode>, ICurrencyCodeRepository
    {
        private readonly EFCoreDbContext _efCoreDbContext;

        public CurrencyCodeRepository(EFCoreDbContext efCoreDbContext) : base(efCoreDbContext)
        {
            _efCoreDbContext = efCoreDbContext;
        }

        public Task TruncateAsync(string tableName)
        {
            return _efCoreDbContext.Database.ExecuteSqlCommandAsync(string.Format("TRUNCATE TABLE public.\"{0}\"", tableName));
        }
    }
}
