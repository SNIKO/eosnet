using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class TableQuery
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("table_key")]
        public string TableKey { get; set; }

        [JsonProperty("lower_bound")]
        public string LowerBound { get; set; } = "0";

        [JsonProperty("upper_bound")]
        public string UpperBound { get; set; } = "-1";

        [JsonProperty("limit")]
        public uint Limit { get; set; } = 10;

        [JsonProperty("json")]
        public bool Json => true;
    }

    public class Table
    {
        [JsonProperty("rows")]
        public List<Dictionary<string, object>> Rows { get; set; }

        [JsonProperty("more")]
        public bool More { get; set; }
    }
}