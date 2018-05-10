using Rapsody.Api.DB;
using Rapsody.Api.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rapsody.Api.Services
{
    public class MagnitudeRepository : Repository<Magnitude>, IMagnitudeRepository
    {
        private readonly RapsodyDbContext _rapsodyDbContext;

        public MagnitudeRepository(RapsodyDbContext rapsodyDbContext) : base(rapsodyDbContext)
        {
            _rapsodyDbContext = rapsodyDbContext;
        }

        public async Task<Magnitude> GetAsync(int id)
        {
            return await _rapsodyDbContext.Magnitude.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
