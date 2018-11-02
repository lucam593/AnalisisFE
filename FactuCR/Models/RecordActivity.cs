using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class RecordActivity
    {
        public int IdRecord { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public string UserEntityIdEntity { get; set; }

        public User UserEntityIdEntityNavigation { get; set; }
    }
}
