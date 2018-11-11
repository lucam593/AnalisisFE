using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ClientType
    {
        public int IdClientType { get; set; }
        public uint UsersIdUser { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Users UsersIdUserNavigation { get; set; }
    }
}
