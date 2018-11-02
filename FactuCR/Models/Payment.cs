using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Bill = new HashSet<Bill>();
        }

        public int IdPayment { get; set; }
        public string Name { get; set; }

        public ICollection<Bill> Bill { get; set; }
    }
}
