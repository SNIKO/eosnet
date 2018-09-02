using System.Collections.Generic;
using System.Threading.Tasks;
using EOS.Client.Models;

namespace EOS.Client
{
    public interface IEosApi
    {
        Task<ChainInfo> GetInfoAsync();

        Task<Block> GetBlockAsync(string blockNumOrId);

        Task<Account> GetAccountAsync(string accountName);

        Task<Code> GetCodeAsync(string accountName);

        Task<Table> GetTableRowsAsync(TableQuery query);

        Task<IEnumerable<string>> GetCurrencyBalanceAsync(string code, string accountName, string symbol);

        Task<IDictionary<string, CurrencyInfo>> GetCurrencyStatsAsync(string code, string symbol);

        Task<AbiInfo> GetAbiAsync(string accountName);

        Task<AbiJsonToBinResult> AbiJsonToBinAsync(string code, string action, IDictionary<string, object> args);

        Task<AbiBinToJsonResult> AbiBinToJsonAsync(string code, string action, string binArgs);

        Task<GetRequiredKeysResult> GetRequiredKeysAsync(Transaction transaction, IEnumerable<string> availableKeys);

        Task<PushTransactionResult> PushTransactionAsync(SignedTransaction transaction);

        Task<IEnumerable<PushTransactionResult>> PushTransactionsAsync(IEnumerable<SignedTransaction> transactions);
    }
}