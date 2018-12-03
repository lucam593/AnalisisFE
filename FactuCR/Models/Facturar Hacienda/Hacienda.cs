using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCR.Models.Facturar_Hacienda
{
    public class Hacienda
    {

        private readonly db_facturacionContext _context;
        

        public Hacienda(db_facturacionContext context)
        {
            _context = context;
           
        }

        public void SendHacienda(string[] valToken, string xFirmado, string clave, string vaucherType, string[] recp)
        {
            ConfigCompany configC = _context.ConfigCompany.Find(1);

            var values = new Dictionary<string, string>
                {
                   { "token", valToken[0]},
                   { "clave", clave }, //clave de factura
                   { "fecha", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss%K") },
                   { "emi_tipoIdentificacion", "0" + Convert.ToString( configC.IdType)},
                   { "emi_numeroIdentificacion", Convert.ToString( configC.IdUser).Replace("-","") },
                  
                   { "comprobanteXml", xFirmado },
                   { "client_id", "api-stag" }
                };

            if (vaucherType.Equals("FE"))
            {
                values.Add("recp_tipoIdentificacion", GenTipoId(recp[0])); //tipo cedula cliente
                values.Add("recp_numeroIdentificacion", Convert.ToString(recp[1]).Replace("-", "")  ); //cedula cliente
            }

            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "send", "json");
            var status = (string)jObjet["Status"];
            var xmlR = (string)jObjet["Status"];
            if (status.Equals("202"))
            {
                //consultarF(valToken[0], clave);
            }
        }


        public void consultarF(string valToken,string clave)
        {
            var values = new Dictionary<string, string>
                {
                   { "token", valToken},
                   { "clave", clave }, //clave de factura
                    { "client_id", "api-stag"}
                };

            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "consultar", "consultarCom");
            var status = (string)jObjet["ind-estado"];
            var xmlRe = (string)jObjet["respuesta-xml"];

            var xmlDecode = Encoding.UTF8.GetString(Convert.FromBase64String(xmlRe));

            //GENERAR XML FIRMADO PARA ENVIAR A CORREO, CORREGIR PATH
            System.IO.File.WriteAllText(@"C:\Users\LucamPC\Desktop\" + clave + ".xml", xmlDecode);
        }
        //------------ DEBE CUIDARSE EL TIEMPO DE DURACION DEL TOKEN
        public string[] Token()
        {
            var configC = _context.ConfigCompany.Find(1);
            var values = new Dictionary<string, string>
                {
                   { "grant_type", "password" },
                   { "client_id", "api-stag" },
                   { "username", configC.UserTax },
                   { "password",  configC.PasswordTax}
                };

            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "token", "gettoken");

            var access_token = (string)jObjet["access_token"];
            var refresh_token = (string)jObjet["refresh_token"];
            var expires_in = (string)jObjet["expires_in"];

            string[] vals = { access_token, refresh_token, expires_in };

            return vals;
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

        public void CreateKey(int idKey, string tipoDocumento, string situacion, string consec, string codSeg, string ter, string sucr)
        {
            ConfigCompany configC = _context.ConfigCompany.Find(1);
            var values = new Dictionary<string, string>
                {
                   { "tipoCedula", "0" + configC.IdType },
                   { "cedula",  Convert.ToString( configC.IdUser).Replace("-","") },
                   { "situacion", situacion },
                   { "codigoPais", "506" },
                   { "consecutivo", consec },
                   { "codigoSeguridad", codSeg },
                   { "tipoDocumento", tipoDocumento },
                   { "terminal", ter },
                   { "sucursal", sucr }
                };
            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "clave", "clave");
            var key = (string)jObjet["clave"];
            var consecutive = (string)jObjet["consecutivo"];

            MasterInvoiceVoucher miv = _context.MasterInvoiceVoucher.Find(idKey);

            miv.ApiKey = key;
            miv.ApiConsecutive = consecutive;

            _context.SaveChanges();
        }

    }
}
