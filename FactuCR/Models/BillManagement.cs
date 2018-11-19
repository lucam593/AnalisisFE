﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class BillManagement
    {
        public MasterInvoiceVoucher MasterInvoiceVoucher { get; set; }

        public List<Product> SelectedProducts = new List<Product>();

        public Product product { get; set; }

        public int IdProduct { get; set; }
    }
}
