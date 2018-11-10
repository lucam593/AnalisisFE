using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Address
    {
        public string IdAddres { get; set; }
        public uint IdUser { get; set; }
        public int IdAddress { get; set; }
        public string OtherSigns { get; set; }

        public MasterAddress IdAddressNavigation { get; set; }
        public Users IdUserNavigation { get; set; }
    }
}
