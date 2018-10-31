using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class SaleCondition
    {
        public SaleCondition()
        {
            Bill = new HashSet<Bill>();
        }

        public int IdCondition { get; set; }
        public string Name { get; set; }

        public ICollection<Bill> Bill { get; set; }
    }
}
