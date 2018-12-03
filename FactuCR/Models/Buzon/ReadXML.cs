using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FactuCR.Models.Buzon
{
    public class ReadXML
    {

        public FacturaBuzon Read()
        {

            FacturaBuzon factura = new FacturaBuzon();
            XDocument xDoc = XDocument.Load("c:/miprimerxml.xml");


            var usuario = from usu in xDoc.Descendants("Clave") select usu;
            
            foreach (XElement clave in usuario.Elements("Clave"))
            {
                Console.Write(clave.Element("Clave").Value);
            }

            return factura;
        }

    }
}
