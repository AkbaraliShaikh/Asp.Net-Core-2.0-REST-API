using Rapsody.Api.Models;
using System.Threading.Tasks;

namespace Rapsody.Api.Services
{
    public interface IMagnitudeRepository : IRepository<Magnitude>, IDatabaseOperation
    {
        Task<Magnitude> GetAsync(int id);
    }
}
