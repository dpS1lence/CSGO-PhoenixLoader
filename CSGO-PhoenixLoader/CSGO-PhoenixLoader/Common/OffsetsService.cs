using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using Newtonsoft.Json;

namespace CSGO_PhoenixLoader.Common
{
    public static class OffsetsService
    {
        public static Offsets? GetAllOffsetsJson(string url)
        {
            var client = new HttpClient();

            string result;

            using (var response = client.GetAsync(url).Result)
            {
                using var content = response.Content;
                result = content.ReadAsStringAsync().Result;
            }

            return JsonConvert.DeserializeObject<Offsets>(result);
        }
    }
}
