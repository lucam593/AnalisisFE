using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterTransmitter
    {
        public int IdTransmitter { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int KindId { get; set; }
        public string BussinessName { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }
        public int Province { get; set; }
        public int Canton { get; set; }
        public int District { get; set; }
        public string Neighborhood { get; set; }
        public string OtherSigns { get; set; }
        public string TelephonoFax { get; set; }
    }
}
