using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Document
    {
        public Document()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int IdDocument { get; set; }
        public string Name { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
