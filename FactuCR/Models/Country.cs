using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class Country
    {
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
        public string CountryISOCode { get; set; }
        public string CountryCode { get; set; }
    }
}
