using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class SituationDocument
    {
        public SituationDocument()
        {
            MasterKey = new HashSet<MasterKey>();
        }

        public int IdSituation { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<MasterKey> MasterKey { get; set; }
    }
}
