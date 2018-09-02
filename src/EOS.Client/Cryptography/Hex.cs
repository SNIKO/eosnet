using System;

namespace EOS.Client.Cryptography
{
    public static class Hex
    {
        public static string Encode(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }
        
        public static byte[] Decode(string hexString)
        {
            var hex = hexString.Replace("-", "");            
            var bytes = new byte[hex.Length / 2];
            
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            
            return bytes;
        }       
    }
}