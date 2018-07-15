using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class CurrencyInfo
    {
        [JsonProperty("supply")]
        public string Supply { get; set; }

        [JsonProperty("max_supply")]
        public string MaxSupply { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }
    }
}