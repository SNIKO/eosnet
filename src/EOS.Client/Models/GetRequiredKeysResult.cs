using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class GetRequiredKeysResult
    {
        [JsonProperty("required_keys")]
        public IEnumerable<string> RequiredKeys { get; set; }
    }
}