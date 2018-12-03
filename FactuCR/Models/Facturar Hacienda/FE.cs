using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCR.Models.Facturar_Hacienda
{
    public class FE
    {

        private readonly db_facturacionContext _context;


        public FE(db_facturacionContext context)
        {
            _context = context;

        }

        public string[] CreateXML(List<MasterDetail> mdList, int idKey, string voucherType)
        {
            MasterInvoiceVoucher miv = _context.MasterInvoiceVoucher.Find(idKey);
            ConfigCompany configC = _context.ConfigCompany.Find(1);
            MasterKey mk = _context.MasterKey.Find(idKey);
            MasterConsecutive mc = _context.MasterConsecutive.Find(idKey);
            MasterReceiver mr = _context.MasterReceiver.Find(idKey);
            List<MasterDetail> listaDetalle = mdList; // DEBE SER LA LISTA DE DETALLE

            String payment = _context.MasterPayment.Find(miv.IdPayment).Code;
            string condition = _context.MasterSaleCondition.Find(miv.IdCondition).Code;

            double total_serv_gravados = 0;
            double total_serv_exentos = 0;
            double total_merc_gravada = 0;
            double total_merc_exenta = 0;
            double total_gravados = 0;
            double total_exentos = 0;
            double total_ventas = 0;
            double total_descuentos = 0;
            double total_ventas_neta = 0;
            double total_impuestos = 0;
            double total_comprobante = 0;

            string detalle = "{";
            int count = 0;
            foreach (var item in listaDetalle)
            {
                count++;
                if (count > 1)
                {
                    detalle += ",";
                }
                detalle += "\"" + count + "\":{ " +
                                                "\"cantidad\":\"" + item.Quantity + "\"," +
                                                "\"unidadMedida\":\"" + item.MeasuredUnitSymbology + "\"," +
                                                "\"detalle\":\"" + item.NameProduct + "\"," +
                                                "\"precioUnitario\":\"" + item.UnitPrice + "\"," +
                                                "\"montoTotal\":\"" + item.TotalAmount + "\"," +
                                                "\"subtotal\":\"" + item.Subtotal + "\"," +
                                                "\"montoTotalLinea\":\"" + item.TotalLineAmount + "\"," +
                                                "\"montoDescuento\":\"" + item.DiscountAmount + "\"," +
                                                "\"naturalezaDescuento\":\"Oferta\"}";

                //------------- Total Amounts Calculation -----------------
                total_descuentos += item.DiscountAmount;
                if (item.MeasuredUnitSymbology.Equals("Sp"))
                {
                    total_serv_exentos += item.TotalLineAmount;
                }
                else
                {
                    total_merc_exenta += item.TotalLineAmount;
                }
            }
            detalle += "}";

            //------------- Total Amounts Calculation -----------------
            total_gravados = total_serv_gravados + total_merc_gravada;
            total_exentos = total_serv_exentos + total_merc_exenta;
            total_ventas = total_gravados + total_exentos;
            total_ventas_neta = total_ventas - total_descuentos;
            total_comprobante = total_ventas_neta + total_impuestos;

            Dictionary<string, string> values = new Dictionary<string, string>
                {
                   { "clave",  miv.ApiKey.ToString()},
                   { "consecutivo", miv.ApiConsecutive.ToString() },
                   { "fecha_emision",  DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss%K") },
                   { "emisor_nombre", configC.FullName },
                   { "emisor_tipo_indetif", "0" + Convert.ToString( configC.IdType) }, // se le suma un cero por estructura de esquema
                   { "emisor_num_identif", Convert.ToString( configC.IdUser).Replace("-","") },
                   { "nombre_comercial", configC.CompannyName },
                   { "emisor_provincia", configC.Province },
                   { "emisor_canton", configC.Canton },
                   { "emisor_distrito", configC.District },
                   { "emisor_barrio", configC.neighborhood },
                   { "emisor_otras_senas", configC.OtherSigns },
                   { "emisor_cod_pais_tel", configC.Country },
                   { "emisor_tel",  Convert.ToString( configC.Telephone).Replace("-","") },
                   { "emisor_cod_pais_fax", configC.Country },
                   { "emisor_fax", Convert.ToString( configC.Fax).Replace("-","") },
                   { "emisor_email", configC.Email },
                   { "receptor_nombre", mr.Fullname }, //TODO REFERENTE A CLIENTE
                   { "receptor_tipo_identif", GenTipoId(mr.IdentificationType) },
                   { "receptor_num_identif", Convert.ToString( mr.IdentificationNumber).Replace("-","") },
                   { "receptor_provincia", "1" },                           //SE DEBEN CAMBIAR
                   { "receptor_canton", "01" },
                   { "receptor_distrito", "03" },
                   { "receptor_barrio", "01" },
                   { "receptor_cod_pais_tel",  mk.Country },
                   { "receptor_tel", mr.Telephone },
                   { "receptor_cod_pais_fax", mk.Country }, //eje: 506
                   { "receptor_fax", mr.Telephone },
                   { "receptor_email", mr.Email },
                   { "condicion_venta", condition }, // eje: 01
                   { "plazo_credito", "0" }, // 
                   { "medio_pago", payment }, // medio pago 01
                   { "cod_moneda", "CRC" }, //eje CRC
                   { "tipo_cambio", Convert.ToString(configC.CurrencyValue) },
                   { "total_serv_gravados", total_serv_gravados.ToString() },
                   { "total_serv_exentos", total_serv_exentos.ToString() },
                   { "total_merc_gravada", total_merc_gravada.ToString() },
                   { "total_merc_exenta", total_merc_exenta.ToString() },
                   { "total_gravados", total_gravados.ToString() },
                   { "total_exentos", total_exentos.ToString() },
                   { "total_ventas", total_ventas.ToString() },
                   { "total_descuentos", total_descuentos.ToString() },
                   { "total_ventas_neta", total_ventas_neta.ToString() },
                   { "total_impuestos", total_impuestos.ToString() },
                   { "total_comprobante", total_comprobante.ToString() },
                   { "otros", "" },
                   { "otrosType", "" }
                };

            values.Add("detalles", detalle);

            ApiConnect api = new ApiConnect();

            
            JToken jObjet = api.PostApi(values, "genXML", "gen_xml_"+ mc.VoucherType.ToLower()); //revisar aca
            var clave = (string)jObjet["clave"];
            var xml = (string)jObjet["xml"];

            //Debug.WriteLine("-------------------------------------------------------------------------------");
            //Debug.WriteLine(xml);

            string[] vals = { clave, xml, mc.VoucherType };

           return  CreateSignXML(vals);
        }

        private string[] CreateSignXML(string[] vals)
        {
            ConfigCompany configC = _context.ConfigCompany.Find(1);
            var file = _context.Files.Find(UInt32.Parse("1"));
            var values = new Dictionary<string, string>
                {
                   { "p12Url", file.DownloadCode },
                   { "inXml", vals[1] },
                   { "pinP12", configC.pin },
                   { "tipodoc", vals[2] }
                };
            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "signXML", "signFE");
            var xmlFirmado = (string)jObjet["xmlFirmado"];

            var xmlDecode = Encoding.UTF8.GetString(Convert.FromBase64String(xmlFirmado));

            //GENERAR XML FIRMADO PARA ENVIAR A CORREO, CORREGIR PATH
            //System.IO.File.WriteAllText(@"C:\Users\LucamPC\Desktop\" + "miprimerxml.xml", xmlDecode);

            string[] vales = {xmlFirmado, vals[0]};
            return vales;

        }


            private string GenTipoId(string id)
        {
            //"fisico", "juridico", "dimex", "nite", "01", "02", "03", "04"
            if (id.Equals("fisico"))
            {
                return "01";
            }
            else if (id.Equals("juridico"))
            {
                return "02";
            }
            else if (id.Equals("dimex"))
            {
                return "03";
            }
            else if (id.Equals("nite"))
            {
                return "04";
            }
            else
            {
                return "99";
            }

        }
    }
}
