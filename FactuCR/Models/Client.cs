using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Client
    {
        public uint IdClient { get; set; }
        public uint IdUser { get; set; }
        public int? IdClientType { get; set; }
        public string KindClient { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public uint Timestamp { get; set; }
    }
}
