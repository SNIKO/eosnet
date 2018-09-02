using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EOS.Client.Cryptography;
using EOS.Client.Models;
using EOS.Client.Utils;
using Action = EOS.Client.Models.Action;

namespace EOS.Client
{
    internal class EosBinarySerializer
    {
        public EosBinarySerializer(IEosApi api)
        {
            this.Api = api;
        }
        
        public async Task<byte[]> SerializeAsync(Transaction transaction)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                // Header
                writer.Write((int) transaction.Expiration.ToUnixTime());
                writer.Write((short) (transaction.RefBlockNum & 0xFFFF));
                writer.Write((int) (transaction.RefBlockPrefix & 0xFFFFFFFF));
                writer.WriteVarUInt32((uint)transaction.MaxNetUsageWords);
                writer.WriteVarUInt32((uint)transaction.MaxCpuUsageMs);
                writer.WriteVarUInt32(transaction.DelaySec);

                // Context Free Actions
                writer.WriteVarUInt32(0);

                // Actions
                var actions = transaction.Actions?.ToArray() ?? new Action[0];
                writer.WriteVarUInt32((uint)actions.Length);
                foreach (var action in actions)
                {
                    await WriteAsync(writer, action);
                }

                // Extensions
                writer.WriteVarUInt32(0);
                
                return stream.ToArray();
            }               
        }
        
        async Task WriteAsync(BinaryWriter writer, Action action)
        {
            writer.Write(Base32.Encode(action.Account));
            writer.Write(Base32.Encode(action.Name));

            // Authorizations
            var authorizations = action.Authorization?.ToArray() ?? new Authorization[0];
            writer.WriteVarUInt32((uint) authorizations.Length);
            foreach (var auth in authorizations)
            {
                Write(writer, auth);
            }

            // Arguments
            var hexData = action.HexData;            
            if (string.IsNullOrEmpty(hexData))
            {                
                var args = await Api.AbiJsonToBinAsync(action.Account, action.Name, action.Data);
                hexData = args.BinArgs;
            }

            var bytes = Hex.Decode(hexData);
            writer.WriteVarUInt32((byte)bytes.Length);
            writer.Write(bytes);                     
        }

        void Write(BinaryWriter writer, Authorization auth)
        {
            writer.Write(Base32.Encode(auth.Actor));
            writer.Write(Base32.Encode(auth.Permission));
        }
        
        IEosApi Api { get; }
    }
}