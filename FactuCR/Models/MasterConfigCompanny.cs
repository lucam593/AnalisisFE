using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterConfigCompanny
    {
        public int IdConfig { get; set; }
        public uint IdUser { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string CompannyName { get; set; }
        public string UserTax { get; set; }
        public string PasswordTax { get; set; }
        public string Coin { get; set; }

        public Users IdUserNavigation { get; set; }
    }
}
