using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterTransmitter
    {
        public int IdKey { get; set; }
        public int IdCard { get; set; }
        public string IdType { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string IdProvince { get; set; }
        public string IdCanton { get; set; }
        public string IdDistrict { get; set; }
        public string IdNeighborhood { get; set; }
        public string OthersSigns { get; set; }
        public string Telephone { get; set; }
        public string CountryCode { get; set; }
        public string Fax { get; set; }

        public MasterInvoiceVoucher IdKeyNavigation { get; set; }
    }
}
