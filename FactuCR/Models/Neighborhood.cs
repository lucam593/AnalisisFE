using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class Neighborhood
    {
        public string NameNeighborhood { get; set; }

        public Neighborhood(string name)
        {
            NameNeighborhood = name;
        }
    }
}
