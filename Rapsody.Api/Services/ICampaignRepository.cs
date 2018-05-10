using Rapsody.Api.Models;
using System.Threading.Tasks;

namespace Rapsody.Api.Services
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<Campaign> GetAsync(int id);
    }
}
