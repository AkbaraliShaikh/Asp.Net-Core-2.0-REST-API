using System.Collections.Generic;
using System.IO;

namespace AspNetCore.Api.Services
{
    public interface ICsvService
    {
        IList<T> Get<T>(TextReader stream) where T : class;
    }
}
