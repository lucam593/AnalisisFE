using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ConfigCompany
    {
        public int IdConfig { get; set; }
        public string FullName { get; set; }
        public int IdType { get; set; }
        public string IdUser { get; set; }
        public string CompannyName { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }
        public string Fax { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string District { get; set; }
        public string OtherSigns { get; set; }
        public string Country { get; set; }
        public string UserTax { get; set; }
        public string PasswordTax { get; set; }
        public string Currency { get; set; }
        public double CurrencyValue { get; set; }

        public IdentificationType IdTypeNavigation { get; set; }
    }
}
