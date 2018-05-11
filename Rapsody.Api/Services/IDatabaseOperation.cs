using System.Threading.Tasks;

namespace Rapsody.Api.Services
{
    public interface IDatabaseOperation
    {
        Task TruncateAsync(string tableName);
    }
}
