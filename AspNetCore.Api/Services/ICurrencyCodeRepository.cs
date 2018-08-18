using AspNetCore.Api.Models;
using System.Threading.Tasks;

namespace AspNetCore.Api.Services
{
    public interface ICurrencyCodeRepository : IRepository<CurrencyCode>, IDatabaseOperation
    {
    }
}
