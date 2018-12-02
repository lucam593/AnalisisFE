using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class ApiConnect
    {
        private static readonly HttpClient client = new HttpClient();
        Uri url = new Uri("https://apicrlibre.000webhostapp.com/apiCRLibre/www/api.php");
        public JToken PostApi(Dictionary<string, string> values, string module, string action)
        {
            values.Add("w", module);
            values.Add("r", action);
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(url, content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(responseString);
            JToken jResult = jObject["resp"];
            return jResult;
        }
        public JToken PutApi(Dictionary<string, string> values, string module, string action)
        {
            values.Add("w", module);
            values.Add("r", action);
            var content = new FormUrlEncodedContent(values);
            var response = client.PutAsync(url, content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(responseString);
            JToken jResult = jObject["resp"];
            return jResult;
        }

        //public JToken PostApiFile()
        //{
        //    var values = new Dictionary<string, object>{};
        //    values.Add("w", "fileUploader");
        //    values.Add("r", "subir_certif");
        //    var content = new FormUrlEncodedContent(values);
        //    var response = client.PostAsync(url, content).Result;
        //    var responseString = response.Content.ReadAsMultipartAsync().Result;
        //    JObject jObject = JObject.Parse(responseString);
        //    JToken jResult = jObject["resp"];
        //    return jResult;
        //}
    }
}
