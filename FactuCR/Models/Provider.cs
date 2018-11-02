using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Provider
    {
        public Provider()
        {
            CommerciallBrand = new HashSet<CommerciallBrand>();
        }

        public string Description { get; set; }
        public string EntityIdEntity { get; set; }

        public Entity EntityIdEntityNavigation { get; set; }
        public ICollection<CommerciallBrand> CommerciallBrand { get; set; }
    }
}
