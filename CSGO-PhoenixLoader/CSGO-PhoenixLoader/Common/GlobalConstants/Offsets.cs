using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSGO_PhoenixLoader.Common.GlobalConstants
{
    public class Offsets
    {
        [JsonProperty("timestamp")]
        [JsonPropertyName("timestamp")]
        public int? Timestamp { get; set; }

        [JsonProperty("signatures")]
        [JsonPropertyName("signatures")]
        public Signatures? Signatures { get; set; }

        [JsonProperty("netvars")]
        [JsonPropertyName("netvars")]
        public Netvars? Netvars { get; set; }
    }
}
