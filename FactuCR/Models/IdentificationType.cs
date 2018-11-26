using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class IdentificationType
    {
        public IdentificationType()
        {
            Client = new HashSet<Client>();
            ConfigCompany = new HashSet<ConfigCompany>();
            Provider = new HashSet<Provider>();
        }

        public int IdType { get; set; }
        public string Code { get; set; }
        public string Symbology { get; set; }
        public string Name { get; set; }

        public ICollection<Client> Client { get; set; }
        public ICollection<ConfigCompany> ConfigCompany { get; set; }
        public ICollection<Provider> Provider { get; set; }
    }
}
