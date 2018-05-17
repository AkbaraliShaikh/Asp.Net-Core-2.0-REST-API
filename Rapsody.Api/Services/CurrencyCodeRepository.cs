using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rapsody.Api.DB;
using Rapsody.Api.Models;

namespace Rapsody.Api.Services
{
    public class CurrencyCodeRepository : Repository<CurrencyCode>, ICurrencyCodeRepository
    {
        private readonly RapsodyDbContext _rapsodyDbContext;

        public CurrencyCodeRepository(RapsodyDbContext rapsodyDbContext) : base(rapsodyDbContext)
        {
            _rapsodyDbContext = rapsodyDbContext;
        }

        public Task TruncateAsync(string tableName)
        {
            return _rapsodyDbContext.Database.ExecuteSqlCommandAsync(string.Format("TRUNCATE TABLE public.\"{0}\"", tableName));
        }
    }
}
