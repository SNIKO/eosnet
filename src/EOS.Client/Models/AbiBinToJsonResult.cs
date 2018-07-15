using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class AbiBinToJsonResult
    {
        [JsonProperty("args")]
        public IDictionary<string, object> Args { get; set; }

        [JsonProperty("required_scope")]
        public IEnumerable<string> RequiredScope { get; set; }

        [JsonProperty("required_auth")]
        public IEnumerable<string> RequiredAuth { get; set; }
    }
}