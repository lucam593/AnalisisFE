using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class District
    {
        public string NameDistrict { get; set; }

        public District(string name)
        {
            this.NameDistrict = name;
        }
    }
}
