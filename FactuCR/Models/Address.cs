using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Address
    {
        public int IdCodificacion { get; set; }
        public uint IdUser { get; set; }
        public string OtherSigns { get; set; }

        public MasterAddress IdCodificacionNavigation { get; set; }
    }
}
