using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterBranchOffice
    {
        public MasterBranchOffice()
        {
            MasterTerminal = new HashSet<MasterTerminal>();
        }

        public int IdBranchOffice { get; set; }
        public uint IdUser { get; set; }
        public string NameBranchOffice { get; set; }
        public string NumberBranchOffice { get; set; }

        public Users IdUserNavigation { get; set; }
        public ICollection<MasterTerminal> MasterTerminal { get; set; }
    }
}
