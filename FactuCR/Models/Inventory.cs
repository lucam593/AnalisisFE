using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Inventory
    {
        public int IdInventary { get; set; }
        public uint IdUser { get; set; }
        public int IdProduct { get; set; }
        public string MovementType { get; set; }
        public int Cuantity { get; set; }
        public DateTime Date { get; set; }

        public Product IdProductNavigation { get; set; }
        public Users IdUserNavigation { get; set; }
    }
}
