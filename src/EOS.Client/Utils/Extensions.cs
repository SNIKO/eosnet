using System;
using System.IO;
using System.Linq;
using EOS.Client.Cryptography;

namespace EOS.Client.Utils
{
    static class Extensions
    {
        public static byte[] Flattern(this byte[][] arrays)
        {
            var res = new byte[arrays.Sum(a => a.Length)];
            var index = 0;
            
            foreach (var array in arrays)
            {
                array.CopyTo(res, index);
                index += array.Length;
            }

            return res;
        }

        public static long ToBase32(this string data)
        {
            var base32 = Base32.Encode(data);
            return base32;
        }
        
        public static string ToBase58(this byte[] data)
        {
            var base58 = Base58.Encode(data);
            return base58;
        }

        public static long ToUnixTime(this DateTime date)
        {
            return new DateTimeOffset(date).ToUnixTimeSeconds();
        }

        public static void WriteVarUInt32(this BinaryWriter writer, uint value)
        {
            while (true)
            {
                var val = (byte) (value & 0x7f);
                var remaining = (byte) (value >> 7);
                
                if (remaining > 0)
                {
                    writer.Write((byte) (0x80 | val));
                }
                else
                {
                    writer.Write(val);
                    break;
                }
            }
        }
    }
}