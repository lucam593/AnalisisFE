using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Address
    {
        public int IdAddress { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Neighborhood { get; set; }
        public string OtherSigns { get; set; }
        public string EntityIdEntity { get; set; }

        public Entity EntityIdEntityNavigation { get; set; }
    }
}
