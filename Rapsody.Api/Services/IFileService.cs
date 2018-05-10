using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Rapsody.Api.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
