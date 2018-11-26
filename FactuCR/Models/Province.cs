using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class Province
    {
        public string NameProvince { get; set; }

        public Province(string name)
        {
            NameProvince = name;
        }
    }
}
