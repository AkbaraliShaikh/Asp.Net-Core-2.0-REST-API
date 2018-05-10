using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rapsody.Api.Services
{
    public class CsvService : ICsvService
    {
        public IList<T> Get<T>(StreamReader stream) where T : class
        {
            var csv = new CsvReader(stream);
            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;
            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }
}
