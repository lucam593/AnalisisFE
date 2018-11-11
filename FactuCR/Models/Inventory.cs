using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Inventory
    {
        public int IdInventary { get; set; }
        public string CodeProduct { get; set; }
        public uint IdUser { get; set; }
        public string MovementType { get; set; }
        public int Cuantity { get; set; }
        public DateTime Date { get; set; }

        public Product Product { get; set; }
    }
}
