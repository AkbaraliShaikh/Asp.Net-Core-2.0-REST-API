using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rapsody.Api.Models
{
    public class Magnitude
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public string Acronym { get; set; }
        public string Currency { get; set; }
    }
}
