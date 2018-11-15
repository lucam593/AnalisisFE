using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Terminal
    {
        public int IdTerminal { get; set; }
        public string Name { get; set; }
        public int TerminalNumber { get; set; }
        public int IdOffice { get; set; }

        public BranchOffice IdOfficeNavigation { get; set; }
    }
}
