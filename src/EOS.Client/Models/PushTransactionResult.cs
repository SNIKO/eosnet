using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class PushTransactionResult
    {
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("processed")]
        public object Processed { get; set; }
    }
}