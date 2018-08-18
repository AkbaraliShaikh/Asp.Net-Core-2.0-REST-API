using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCore.Api.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFile(string path);
    }
}
