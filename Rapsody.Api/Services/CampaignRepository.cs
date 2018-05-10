﻿using Rapsody.Api.DB;
using Rapsody.Api.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rapsody.Api.Services
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        private readonly RapsodyDbContext _rapsodyDbContext;

        public CampaignRepository(RapsodyDbContext rapsodyDbContext) : base(rapsodyDbContext)
        {
            _rapsodyDbContext = rapsodyDbContext;
        }

        public async Task<Campaign> GetAsync(int id)
        {
            return await _rapsodyDbContext.Campaign.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}