using System.Collections.Generic;
using System.IO;

namespace Rapsody.Api.Services
{
    public interface ICsvService
    {
        IList<T> Get<T>(StreamReader stream) where T : class;
    }
}
