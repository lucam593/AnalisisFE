using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class TelephoneContact
    {
        public int IdContact { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int CountryCode { get; set; }
        public int? Extension { get; set; }
        public string EntityIdEntity { get; set; }

        public Entity EntityIdEntityNavigation { get; set; }
    }
}
