using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Inventory
    {
        public int IdInventary { get; set; }
        public string MovementType { get; set; }
        public int Cuantity { get; set; }
        public DateTime Date { get; set; }
        public int ProductIdProduct { get; set; }
        public string ProviderPersonIdPerson { get; set; }

        public Product ProductIdProductNavigation { get; set; }
    }
}
