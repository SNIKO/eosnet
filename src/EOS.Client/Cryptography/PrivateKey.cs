using System;
using System.Linq;
using System.Security.Cryptography;
using Cryptography.ECDSA;
using EOS.Client.Utils;

namespace EOS.Client.Cryptography
{
    class PrivateKey
    {
        public PrivateKey(string key)
        {
            var bytes = Base58.Decode(key);
            var version = bytes[0];
            var ecdsaKey = new byte[bytes.Length - 5];
            var checksum = new byte[4];

            Array.Copy(bytes, 1, ecdsaKey, 0, ecdsaKey.Length);
            Array.Copy(bytes, bytes.Length - 4, checksum, 0, 4);

            if (version != 0x80)
            {
                throw new ArgumentException("The key has invalid version", nameof(key));
            }

            var dataForChecksum = new[]
            {
                new[] {version},
                ecdsaKey
            };
                              
            var calculatedChecksum = GetLegacyChecksum(dataForChecksum.Flattern());            
            if (!calculatedChecksum.SequenceEqual(checksum))
            {
                throw new ArgumentException("Checksum doesn't match", nameof(key));
            }
            
            Key = ecdsaKey;
        }

        public string GetPublicKey()
        {
            var publicKeyBytes = Secp256K1Manager.GetPublicKey(Key, true);            
            var checksum = GetChecksum(publicKeyBytes);
            var keyWithChecksum = new[] {publicKeyBytes, checksum};
            
            var publicKey = Base58.Encode(keyWithChecksum.Flattern());
            return $"EOS{publicKey}";
        }
        
        static byte[] GetLegacyChecksum(byte[] data)
        {
            var sha256 = new SHA256Managed();
            var hash1 = sha256.ComputeHash(data);
            var hash2 = sha256.ComputeHash(hash1);
            
            return hash2.Take(4).ToArray();
        }

        static byte[] GetChecksum(byte[] data)
        {
            var hash = Ripemd160Manager.GetHash(data);
            return hash.Take(4).ToArray();
        }
        
        public byte[] Key { get; }
        
        public string KeyType { get; }
    }
}