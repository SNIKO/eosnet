using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class AbiInfo
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("abi")]
        public Abi Abi { get; set; }
    }

    public class Abi
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("types")]
        public IEnumerable<AbiType> Types { get; set; }

        [JsonProperty("structs")]
        public IEnumerable<AbiStruct> Structs { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<AbiAction> Actions { get; set; }

        [JsonProperty("tables")]
        public IEnumerable<AbiTable> Tables { get; set; }

        [JsonProperty("ricardian_clauses")]
        public IEnumerable<string> RicardianClauses { get; set; }

        [JsonProperty("error_messages")]
        public IEnumerable<string> ErrorMessages { get; set; }
    }

    public class AbiType
    {
        [JsonProperty("new_type_name")]
        public string NewTypeName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class AbiStruct
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("fields")]
        public IEnumerable<AbiField> Fields { get; set; }
    }

    public class AbiField
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class AbiAction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("ricardian_contract")]
        public string RicardianContract { get; set; }
    }

    public class AbiTable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("index_type")]
        public string IndexType { get; set; }

        [JsonProperty("key_names")]
        public IEnumerable<string> KeyNames { get; set; }

        [JsonProperty("key_types")]
        public IEnumerable<string> KeyTypes { get; set; }
    }
}