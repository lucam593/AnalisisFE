using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class BranchOffice
    {
        public BranchOffice()
        {
            Terminal = new HashSet<Terminal>();
        }

        public int IdOffice { get; set; }
        public string Name { get; set; }
        public int OfficeNumber { get; set; }

        public ICollection<Terminal> Terminal { get; set; }
    }
}
