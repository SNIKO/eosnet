using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EOS.Client.Models;

namespace EOS.Client
{
    public class EosClient : IDisposable
    {
        private readonly HttpClient client;

        public EosClient()
            : this("http://localhost:8888")
        {
        }

        public EosClient(string nodeUri)
            : this(new Uri(nodeUri))
        {
        }

        public EosClient(Uri nodeUri)
        {
            client = new HttpClient {BaseAddress = nodeUri};
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        public Task<ChainInfo> GetInfoAsync()
        {
            return client.GetAsync<ChainInfo>("/v1/chain/get_info");
        }


        public Task<Block> GetBlockAsync(string blockNumOrId)
        {
            return client.PostAsync<Block>("/v1/chain/get_block", $"{{\"block_num_or_id\": {blockNumOrId}}}");
        }


        public Task<Account> GetAccountAsync(string accountName)
        {
            return client.PostAsync<Account>("/v1/chain/get_account", $"{{\"account_name\": \"{accountName}\"}}");
        }

        public Task<Code> GetCodeAsync(string accountName)
        {
            return client.PostAsync<Code>("/v1/chain/get_code", $"{{\"account_name\": \"{accountName}\"}}");
        }

        public Task<Table> GetTableRowsAsync(TableQuery query)
        {
            return client.PostAsync<Table>("/v1/chain/get_table_rows", query);
        }

        public Task<IEnumerable<string>> GetCurrencyBalanceAsync(string code, string accountName, string symbol)
        {
            return client.PostAsync<IEnumerable<string>>("/v1/chain/get_currency_balance", new Dictionary<string, object>
            {
                {"code", code},
                {"account", accountName},
                {"symbol", symbol}
            });
        }

        public Task<IDictionary<string, CurrencyInfo>> GetCurrencyStatsAsync(string code, string symbol)
        {
            return client.PostAsync<IDictionary<string, CurrencyInfo>>("/v1/chain/get_currency_stats", new Dictionary<string, object>
            {
                {"code", code},
                {"symbol", symbol}
            });
        }

        public Task<AbiInfo> GetAbiAsync(string accountName)
        {
            return client.PostAsync<AbiInfo>("/v1/chain/get_abi", $"{{\"account_name\": \"{accountName}\"}}");
        }

        public Task<AbiJsonToBinResult> AbiJsonToBinAsync(string code, string action, IDictionary<string, object> args)
        {
            return client.PostAsync<AbiJsonToBinResult>("/v1/chain/abi_json_to_bin", new Dictionary<string, object>
            {
                {"code", code},
                {"action", action},
                {"args", args}
            });
        }

        public Task<AbiBinToJsonResult> AbiBinToJsonAsync(string code, string action, string binArgs)
        {
            return client.PostAsync<AbiBinToJsonResult>("/v1/chain/abi_bin_to_json", new Dictionary<string, object>
            {
                {"code", code},
                {"action", action},
                {"binargs", binArgs}
            });
        }

        public Task<GetRequiredKeysResult> GetRequiredKeysAsync(Transaction transaction, IEnumerable<string> availableKeys)
        {
            return client.PostAsync<GetRequiredKeysResult>("/v1/chain/get_required_keys", new Dictionary<string, object>
            {
                {"transaction", transaction},
                {"available_keys", availableKeys}
            });
        }

        public Task<PushTransactionResult> PushTransactionAsync(SignedTransaction transaction)
        {
            return client.PostAsync<PushTransactionResult>("/v1/chain/push_transaction", transaction);
        }

        public Task<IEnumerable<PushTransactionResult>> PushTransactionsAsync(IEnumerable<SignedTransaction> transactions)
        {
            return client.PostAsync<IEnumerable<PushTransactionResult>>("/v1/chain/push_transactions", transactions);
        }
    }
}