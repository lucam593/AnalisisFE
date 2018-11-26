using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class VoucherType
    {
        /*
        public VoucherType()
        {
            MasterConsecutive = new HashSet<MasterConsecutive>();
        }
        */
        public int IdVoucherType { get; set; }
        public string Code { get; set; }
        public string Symbology { get; set; }
        public string Name { get; set; }

        //public ICollection<MasterConsecutive> MasterConsecutive { get; set; }
    }
}
