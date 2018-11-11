using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterReceiver
    {
        public int IdClave { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string KindId { get; set; }
        public string Telephone { get; set; }
        public string IdProvince { get; set; }
        public string IdCanton { get; set; }
        public string IdDistrict { get; set; }
        public string Idneighborhood { get; set; }
        public string OthersSigns { get; set; }
        public string ComercialName { get; set; }
        public string PrincipalEmail { get; set; }
        public string CountryCode { get; set; }
        public string TelephoneFax { get; set; }
        public int Status { get; set; }
    }
}
