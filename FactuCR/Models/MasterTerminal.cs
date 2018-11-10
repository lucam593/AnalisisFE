using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterTerminal
    {
        public int IdTerminal { get; set; }
        public int IdBranchOffice { get; set; }
        public string NameTerminal { get; set; }
        public string NumberTerminal { get; set; }

        public MasterBranchOffice IdBranchOfficeNavigation { get; set; }
    }
}
