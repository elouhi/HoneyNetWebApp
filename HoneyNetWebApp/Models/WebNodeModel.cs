using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace HoneyNetWebApp.Models
{
    public class WebNodeModel 
    {
        [JsonProperty("DPT")]
        public string DPT { get; set; }
        [JsonProperty("PROTO")]
        public string Protocol { get; set; }
        [JsonProperty("SRC")]
        public string IPAddress { get; set; }
        [JsonProperty("TTL")]
        public string TTL { get; set; }
        [JsonProperty("WINDOW")]
        public string Window { get; set; }
        [JsonProperty("day")]
        public string Day { get; set; }
        [JsonProperty("month")]
        public string Month { get; set; }
        [JsonProperty("node")]
        public string NodeLocation { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonExtensionData]
        public IDictionary<string, JToken> _additionalData;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                DPT, Protocol, IPAddress, TTL, Window, DPT, Month, NodeLocation,
                Time, _additionalData);
        }       
    }



}