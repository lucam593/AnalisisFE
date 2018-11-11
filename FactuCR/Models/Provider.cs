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

        public string IdProvider { get; set; }
        public uint IdUser { get; set; }
        public string Description { get; set; }
        public string NameProvider { get; set; }
        public string KindId { get; set; }
        public string Email { get; set; }

        public Users IdUserNavigation { get; set; }
        public ICollection<CommerciallBrand> CommerciallBrand { get; set; }
    }
}
