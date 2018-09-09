using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOS.Client.Cryptography;
using EOS.Client.Utils;
using Secp256K1Manager = Cryptography.ECDSA.Secp256K1Manager;
using Ripemd160Manager = Cryptography.ECDSA.Ripemd160Manager;
using Sha256Manager = Cryptography.ECDSA.Sha256Manager;

namespace EOS.Client
{
    public class EosWallet : ISignatureProvider
    {
        static readonly byte[] KeyTypeBytes = Encoding.UTF8.GetBytes("K1");

        public EosWallet(IEnumerable<string> privateKeys)
        {
            foreach (var key in privateKeys)
            {
                var privateKey = new PrivateKey(key);
                var publicKey = privateKey.GetPublicKey();

                keysStorage[publicKey] = privateKey;
            }
        }

        public IEnumerable<string> AvailableKeys => keysStorage.Keys.ToArray();
        
        public Task<IEnumerable<string>> SignAsync(byte[] data, IEnumerable<string> requiredKeys)
        {
            var missingKeys = requiredKeys.Where(key => !keysStorage.ContainsKey(key)).ToArray(); 
            if (missingKeys.Any())
            {
                throw new ArgumentException($"There are no private key(s) for public key(s) '{string.Join(", ", missingKeys)}' in the wallet");
            }

            var hash = Sha256Manager.GetHash(data);
            var signatures = requiredKeys.Select(key => SignHash(hash, keysStorage[key])).ToArray();
                        
            return Task.FromResult<IEnumerable<string>>(signatures);
        }

        static string SignHash(byte[] hash, PrivateKey privateKey)
        {
            var signature = Secp256K1Manager.SignCompressedCompact(hash, privateKey.Key);
            var signatureWithChecksum = new[]
            {
                signature,
                GetChecksum(signature)                
            };

            var finalSignature = $"SIG_K1_{signatureWithChecksum.Flattern().ToBase58()}";
            return finalSignature;
        }
        
        static byte[] GetChecksum(byte[] signature)
        {
            var checkBytes = new[]
            {
                signature,
                KeyTypeBytes
            };
            
            var hash = Ripemd160Manager.GetHash(checkBytes.Flattern());           
            return hash.Take(4).ToArray();
        }
        
        readonly Dictionary<string, PrivateKey> keysStorage = new Dictionary<string, PrivateKey>();
    }
}