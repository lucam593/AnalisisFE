using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterConsecutive
    {
        public int IdConsecutivo { get; set; }
        public int BranchOffice { get; set; }
        public int Terminal { get; set; }
        public int VoucherType { get; set; }
        public int NumberVoucher { get; set; }
    }
}
