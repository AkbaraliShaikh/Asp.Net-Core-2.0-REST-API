using System.Collections.Generic;
using System.IO;

namespace Rapsody.Api.Services
{
    public interface ICsvService
    {
        IList<T> Get<T>(TextReader stream) where T : class;
    }
}
