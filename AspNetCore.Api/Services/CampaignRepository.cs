using AspNetCore.Api.DB;
using AspNetCore.Api.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Api.Services
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        private readonly EFCoreDbContext _efCoreDbContext;

        public CampaignRepository(EFCoreDbContext efCoreDbContext) : base(efCoreDbContext)
        {
            _efCoreDbContext = efCoreDbContext;
        }

        public async Task<Campaign> GetAsync(int id)
        {
            return await _efCoreDbContext.Campaign.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
