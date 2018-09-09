using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class TransactionInfo
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("cpu_usage_us")]
        public ulong CpuUsageUs { get; set; }

        [JsonProperty("net_usage_words")]
        public ulong NetUsageWords { get; set; }

        [JsonProperty("trx")]
        public SignedTransaction Trx { get; set; }
    }

    public class SignedTransaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("signatures")]
        public IEnumerable<string> Signatures { get; set; }

        [JsonProperty("compression")]
        public string Compression { get; set; }

        [JsonProperty("packed_trx")]
        public string PackedTransaction { get; set; }
        
        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }
    }

    public class Transaction
    {
        [JsonProperty("expiration")]
        [JsonConverter(typeof(EosDateTimeConverter))]
        public DateTime Expiration { get; set; }

        [JsonProperty("ref_block_num")]
        public uint RefBlockNum { get; set; }

        [JsonProperty("ref_block_prefix")]
        public ulong RefBlockPrefix { get; set; }

        [JsonProperty("max_net_usage_words")]
        public ulong MaxNetUsageWords { get; set; }

        [JsonProperty("max_cpu_usage_ms")]
        public ulong MaxCpuUsageMs { get; set; }

        [JsonProperty("delay_sec")]
        public uint DelaySec { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<Action> Actions { get; set; }
    }

    public class Action
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("authorization")]
        public IEnumerable<Authorization> Authorization { get; set; }

        [JsonProperty("data")]
        public IDictionary<string, object> Data { get; set; }

        [JsonProperty("hex_data")]
        public string HexData { get; set; }
    }

    public class Authorization
    {
        [JsonProperty("actor")]
        public string Actor { get; set; }

        [JsonProperty("permission")]
        public string Permission { get; set; }
    }
}