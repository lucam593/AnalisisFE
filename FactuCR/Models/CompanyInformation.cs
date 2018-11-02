using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class CompanyInformation
    {
        public CompanyInformation()
        {
            Bill = new HashSet<Bill>();
        }

        public string Tradename { get; set; }
        public int BoxNumber { get; set; }
        public int BranchOfficeIdOffice { get; set; }
        public string EntityIdEntity { get; set; }

        public BranchOffice BranchOfficeIdOfficeNavigation { get; set; }
        public Entity EntityIdEntityNavigation { get; set; }
        public ICollection<Bill> Bill { get; set; }
    }
}
