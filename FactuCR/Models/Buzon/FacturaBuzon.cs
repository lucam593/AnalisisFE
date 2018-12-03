using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models.Buzon
{
    public class FacturaBuzon
    {
        public string  Clave { get; set; }
        public double Monto_Total { get; set; }
        public double Monto_Impuestos { get; set; }

        public string IdEmisor { get; set; }
        public string fecha { get; set; }
    }
}
