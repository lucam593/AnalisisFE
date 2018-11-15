using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterConsecutive
    {
        public MasterConsecutive()
        {
            MasterKey = new HashSet<MasterKey>();
        }

        public int IdConsecutive { get; set; }
        public string BranchOffice { get; set; }
        public string Terminal { get; set; }
        public int VoucherType { get; set; }
        public string NumberConsecutive { get; set; }

        public VoucherType VoucherTypeNavigation { get; set; }
        public ICollection<MasterKey> MasterKey { get; set; }
    }
}
