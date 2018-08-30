using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class Block
    {
        [JsonProperty("timestamp")]
        [JsonConverter(typeof(EosDateTimeConverter))]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("producer")]
        public string Producer { get; set; }

        [JsonProperty("confirmed")]
        public int Confirmed { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("transaction_mroot")]
        public string TransactionMRoot { get; set; }

        [JsonProperty("action_mroot")]
        public string ActionMRoot { get; set; }

        [JsonProperty("schedule_version")]
        public int ScheduleVersion { get; set; }

        [JsonProperty("producer_signature")]
        public string ProducerSignature { get; set; }

        [JsonProperty("transactions")]
        public IEnumerable<TransactionInfo> Transactions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("block_num")]
        public uint BlockNum { get; set; }

        [JsonProperty("ref_block_prefix")]
        public uint RefBlockPrefix { get; set; }
    }
}