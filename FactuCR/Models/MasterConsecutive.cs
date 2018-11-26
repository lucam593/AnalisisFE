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
        public string VoucherType { get; set; }
        public string NumberConsecutive { get; set; }
        
        public ICollection<MasterKey> MasterKey { get; set; }
    }
}
