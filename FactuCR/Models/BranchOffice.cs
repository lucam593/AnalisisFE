using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class BranchOffice
    {
        public BranchOffice()
        {
            CompanyInformation = new HashSet<CompanyInformation>();
        }

        public int IdOffice { get; set; }
        public string Name { get; set; }
        public string NumberOffice { get; set; }

        public ICollection<CompanyInformation> CompanyInformation { get; set; }
    }
}
