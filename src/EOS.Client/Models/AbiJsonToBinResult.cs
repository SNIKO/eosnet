using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class AbiJsonToBinResult
    {
        [JsonProperty("binargs")]
        public string BinArgs { get; set; }

        [JsonProperty("required_scope")]
        public IEnumerable<string> RequiredScope { get; set; }

        [JsonProperty("required_auth")]
        public IEnumerable<string> RequiredAuth { get; set; }
    }
}