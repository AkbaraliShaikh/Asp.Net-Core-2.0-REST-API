using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCore.Api.Services
{
    public class CsvService : ICsvService
    {
        public IList<T> Get<T>(TextReader stream) where T : class
        {
            var csv = new CsvReader(stream);
            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;
            csv.Configuration.Delimiter = ";";
            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }
}
