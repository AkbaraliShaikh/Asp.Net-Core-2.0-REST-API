using Rapsody.Api.Models;
using System.Threading.Tasks;

namespace Rapsody.Api.Services
{
    public interface ICurrencyCodeRepository : IRepository<CurrencyCode>, IDatabaseOperation
    {
    }
}
