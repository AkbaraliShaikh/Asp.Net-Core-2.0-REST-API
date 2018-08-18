using System.Threading.Tasks;

namespace AspNetCore.Api.Services
{
    public interface IDatabaseOperation
    {
        Task TruncateAsync(string tableName);
    }
}
