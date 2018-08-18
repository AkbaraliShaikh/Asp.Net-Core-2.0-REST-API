using AspNetCore.Api.Models;
using System.Threading.Tasks;

namespace AspNetCore.Api.Services
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<Campaign> GetAsync(int id);
    }
}
