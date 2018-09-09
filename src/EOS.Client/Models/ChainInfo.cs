using System;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class ChainInfo
    {
        [JsonProperty("server_version")]
        public string ServerVersion { get; set; }

        [JsonProperty("server_version_string")]
        public string ServerVersionString { get; set; }
        
        [JsonProperty("chain_id")]
        public string ChainId { get; set; }
        
        [JsonProperty("head_block_num")]
        public uint HeadBlockNum { get; set; }

        [JsonProperty("last_irreversible_block_num")]
        public uint LastIrreversibleBlockNum { get; set; }

        [JsonProperty("last_irreversible_block_id")]
        public string LastIrreversibleBlockId { get; set; }

        [JsonProperty("head_block_id")]
        public string HeadBlockId { get; set; }

        [JsonProperty("head_block_time")]
        [JsonConverter(typeof(EosDateTimeConverter))]
        public DateTime HeadBlockTime { get; set; }

        [JsonProperty("head_block_producer")]
        public string HeadBlockProducer { get; set; }

        [JsonProperty("virtual_block_cpu_limit")]
        public ulong VirtualBlockCpuLimit { get; set; }

        [JsonProperty("virtual_block_net_limit")]
        public ulong VirtualBlockNetLimit { get; set; }

        [JsonProperty("block_cpu_limit")]
        public ulong BlockCpuLimit { get; set; }

        [JsonProperty("block_net_limit")]
        public ulong BlockNetLimit { get; set; }
    }
}