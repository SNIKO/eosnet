using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class Code
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("code_hash")]
        public string CodeHash { get; set; }

        [JsonProperty("wast")]
        public string Wast { get; set; }

        [JsonProperty("abi")]
        public Abi Abi { get; set; }
    }
}