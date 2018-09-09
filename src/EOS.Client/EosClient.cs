using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EOS.Client.Models;
using EOS.Client.Utils;
using Action = EOS.Client.Models.Action;
using Hex = EOS.Client.Cryptography.Hex;

namespace EOS.Client
{
    public class EosClient
    {
        public EosClient(ISignatureProvider signatureProvider)
            : this(new Uri("http://localhost:8888"), signatureProvider)
        {
        }

        public EosClient(Uri nodeUri, ISignatureProvider signatureProvider)
        {
            this.Api = new EosApi(nodeUri);
            this.signatureProvider = signatureProvider;
            this.serializer = new EosBinarySerializer(this.Api);
        }
        
        public IEosApi Api { get; }

        public async Task<string> PushActionsAsync(IEnumerable<Action> actions)
        {
            var chainInfo = await Api.GetInfoAsync();
            var blockInfo = await Api.GetBlockAsync(chainInfo.LastIrreversibleBlockId);

            var transaction = new Transaction
            {
                RefBlockNum = chainInfo.LastIrreversibleBlockNum,
                RefBlockPrefix = blockInfo.RefBlockPrefix,
                Expiration = chainInfo.HeadBlockTime.AddSeconds(30),
                Actions = actions
            };

            return await PushTransactionAsync(transaction, chainInfo.ChainId);
        }

        public async Task<string> PushTransactionAsync(Transaction transaction)
        {            
            cachedChainInfo = cachedChainInfo ?? await Api.GetInfoAsync();

            return await PushTransactionAsync(transaction, cachedChainInfo.ChainId);
        }
        
        async Task<string> PushTransactionAsync(Transaction transaction, string chainId)
        {
            var packedTransaction = await serializer.SerializeAsync(transaction);

            var signDigest = new[]
            {
                Hex.Decode(chainId),
                packedTransaction,
                new byte[32]
            };
                        
            var keysInfo = await Api.GetRequiredKeysAsync(transaction, signatureProvider.AvailableKeys);
            var signatures = await signatureProvider.SignAsync(signDigest.Flattern(), keysInfo.RequiredKeys);

            var res = await Api.PushTransactionAsync(new SignedTransaction
            {
                Transaction = transaction,
                Signatures = signatures
            });

            return res.TransactionId;
        }

        private readonly ISignatureProvider signatureProvider;
        private readonly EosBinarySerializer serializer;
        ChainInfo cachedChainInfo;
    }
}