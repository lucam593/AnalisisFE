using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Users
    {
        public uint IdUser { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public uint Timestamp { get; set; }
        public uint LastAccess { get; set; }
        public string Pwd { get; set; }
        public string Avatar { get; set; }
        public string Settings { get; set; }
    }
}
