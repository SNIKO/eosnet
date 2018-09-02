using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EOS.Client.Utils;

namespace EOS.Client.Cryptography
{
    public static class Base58
    {
        const string Characters = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        static readonly IDictionary<char, byte> Values;
        static readonly IDictionary<byte, char> Chars;

        static Base58()
        {
            Values = new Dictionary<char, byte>(Characters.Length);
            Chars = new Dictionary<byte, char>(Characters.Length);

            byte val = 0;

            foreach (var ch in Characters)
            {
                Values[ch] = val;
                Chars[val] = ch;

                val++;
            }
        }

        public static string Encode(byte[] data)
        {
            var base58 = string.Empty;
            var intData = BigInteger.Zero;

            for (var i = 0; i < data.Length; i++)
            {
                intData = intData * 256 + data[i];
            }

            while (intData > 0)
            {
                var val = intData % 58;
                var base58Char = Chars[(byte) val];

                base58 = base58Char + base58;

                intData /= 58;
            }

            var leadingZerosCount = data.TakeWhile(b => b == 0).Count();
            base58 = base58.PadLeft(leadingZerosCount, '1');

            return base58;
        }

        public static byte[] Decode(string base58String)
        {
            var intData = BigInteger.Zero;

            foreach (var ch in base58String)
            {
                if (!Values.TryGetValue(ch, out var val))
                {
                    throw new ArgumentException($"Value '{base58String}' contains invalid Base58 character '{ch}'", nameof(base58String));
                }

                intData = intData * 58 + val;
            }

            var bytes = intData.ToByteArray().Reverse().SkipWhile(c => c == 0).ToArray();

            var leadingZerosCount = base58String.TakeWhile(c => c == '1').Count();
            if (leadingZerosCount > 0)
            {
                var leadingZeros = new byte[leadingZerosCount];
                bytes = new[] {leadingZeros, bytes}.Flattern();
            }

            return bytes;
        }
    }
}