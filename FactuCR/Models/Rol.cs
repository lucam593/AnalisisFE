using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolHasUsers = new HashSet<RolHasUsers>();
        }

        public int IdRol { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public ICollection<RolHasUsers> RolHasUsers { get; set; }
    }
}
