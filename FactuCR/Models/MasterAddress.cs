using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterAddress
    {
        public MasterAddress()
        {
            Address = new HashSet<Address>();
        }

        public int IdCodificacion { get; set; }
        public string IdProvincia { get; set; }
        public string NombreProvincia { get; set; }
        public string IdCanton { get; set; }
        public string NombreCanton { get; set; }
        public string IdDistrito { get; set; }
        public string NombreDistrito { get; set; }
        public string IdBarrio { get; set; }
        public string NombreBarrio { get; set; }

        public ICollection<Address> Address { get; set; }
    }
}
