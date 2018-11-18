using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class RolHasUsers
    {
        public int IdRol { get; set; }
        public int IdUsers { get; set; }

        public Rol RolIdRolNavigation { get; set; }
    }
}
