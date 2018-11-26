using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class Canton
    {
        public string NameCanton { get; set; }

        public Canton(string Name)
        {
            this.NameCanton = Name;
        }
    }
}
