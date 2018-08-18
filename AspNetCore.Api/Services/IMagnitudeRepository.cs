using AspNetCore.Api.Models;
using System.Threading.Tasks;

namespace AspNetCore.Api.Services
{
    public interface IMagnitudeRepository : IRepository<Magnitude>, IDatabaseOperation
    {
        Task<Magnitude> GetAsync(int id);
    }
}
