using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Users
    {
        public Users()
        {
            Address = new HashSet<Address>();
            ClientType = new HashSet<ClientType>();
            Files = new HashSet<Files>();
            MasterBranchOffice = new HashSet<MasterBranchOffice>();
            MasterConfigCompanny = new HashSet<MasterConfigCompanny>();
            MasterSessions = new HashSet<MasterSessions>();
            Product = new HashSet<Product>();
            ProductFamily = new HashSet<ProductFamily>();
            Provider = new HashSet<Provider>();
        }

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

        public ICollection<Address> Address { get; set; }
        public ICollection<ClientType> ClientType { get; set; }
        public ICollection<Files> Files { get; set; }
        public ICollection<MasterBranchOffice> MasterBranchOffice { get; set; }
        public ICollection<MasterConfigCompanny> MasterConfigCompanny { get; set; }
        public ICollection<MasterSessions> MasterSessions { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductFamily> ProductFamily { get; set; }
        public ICollection<Provider> Provider { get; set; }
    }
}
