using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class Account
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("head_block_num")]
        public uint HeadBlockNum { get; set; }

        [JsonProperty("head_block_time")]
        public DateTime HeadBlockTime { get; set; }

        [JsonProperty("privileged")]
        public bool Privileged { get; set; }

        [JsonProperty("last_code_update")]
        public DateTime LastCodeUpdate { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("core_liquid_balance")]
        public string CoreLiquidBalance { get; set; }

        [JsonProperty("ram_quota")]
        public ulong RamQuota { get; set; }

        [JsonProperty("net_weight")]
        public ulong NetWeight { get; set; }

        [JsonProperty("cpu_weight")]
        public ulong CpuWeight { get; set; }

        [JsonProperty("net_limit")]
        public ResourceLimit NetLimit { get; set; }

        [JsonProperty("cpu_limit")]
        public ResourceLimit CpuLimit { get; set; }

        [JsonProperty("ram_usage")]
        public ulong RamUssage { get; set; }

        [JsonProperty("permissions")]
        public IEnumerable<Permission> Permissions { get; set; }
    }

    public class ResourceLimit
    {
        [JsonProperty("used")]
        public ulong Used { get; set; }

        [JsonProperty("available")]
        public ulong Available { get; set; }

        [JsonProperty("max")]
        public ulong Max { get; set; }
    }

    public class Permission
    {
        [JsonProperty("perm_name")]
        public string Name { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("required_auth")]
        public RequiredAuth RequiredAuth { get; set; }
    }

    public class RequiredAuth
    {
        [JsonProperty("threshold")]
        public uint Threshold { get; set; }

        [JsonProperty("keys")]
        public IEnumerable<AuthKey> Keys { get; set; }

        [JsonProperty("accounts")]
        public IEnumerable<string> Accounts { get; set; }
    }

    public class AuthKey
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("weight")]
        public uint Weight { get; set; }
    }

    public class TotalResources
    {
        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("net_weight")]
        public ulong NetWeight { get; set; }

        [JsonProperty("cpu_weight")]
        public ulong CpuWeight { get; set; }

        [JsonProperty("ram_bytes")]
        public ulong RamBytes { get; set; }
    }
}