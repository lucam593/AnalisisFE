using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Provider
    {
        public Provider()
        {
            Product = new HashSet<Product>();
        }

        public int IdProvider { get; set; }
        public int IdType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public IdentificationType IdTypeNavigation { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
