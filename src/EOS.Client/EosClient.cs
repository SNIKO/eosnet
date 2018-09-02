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
            this.serializationProvider = new EosBinarySerializer(this.Api);
        }
        
        public IEosApi Api { get; }

        public async Task<string> PushActionsAsync(IEnumerable<Action> actions)
        {
            var chainInfo = await Api.GetInfoAsync();
            var headBlockInfo = await Api.GetBlockAsync(chainInfo.HeadBlockId);
            var transaction = new Transaction
            {
                RefBlockNum = chainInfo.HeadBlockNum,
                RefBlockPrefix = headBlockInfo.RefBlockPrefix,
                Expiration = chainInfo.HeadBlockTime.AddSeconds(30),
                Actions = actions
            };

            return await PushTransactionAsync(transaction);
        }
        
        public async Task<string> PushTransactionAsync(Transaction transaction)
        {
            var packedTransaction = await serializationProvider.SerializeAsync(transaction);
            var chainInfo = await Api.GetInfoAsync();
            var signDigest = new[]
            {
                Hex.Decode(chainInfo.ChainId),
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
        private readonly EosBinarySerializer serializationProvider;
    }
}