using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Inventory
    {
        public int IdInventory { get; set; }
        public int IdProduct { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }

        public Product IdProductNavigation { get; set; }
    }
}
