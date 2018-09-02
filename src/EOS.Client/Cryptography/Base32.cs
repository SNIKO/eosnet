namespace EOS.Client.Cryptography
{
    public static class Base32
    {
        public static long Encode(string data)
        {
            long result = 0;

            for (var i = 0; i <= 12; i++)
            {
                long c = 0;
                
                if (i < data.Length && i <= 12)
                {
                    c = CharToValue(data[i]);
                }

                if (i < 12)
                {
                    c &= 0x1f;
                    c <<= 64 - 5 * (i + 1);
                }
                else
                {
                    c &= 0x0f;
                }

                result |= c;
            }

            return result;
        }
        
        static byte CharToValue(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return (byte)(c - 'a' + 6);
            }
            
            if (c >= '1' && c <= '5')
            {
                return (byte)(c - '1' + 1);
            }

            return 0;
        }
    }
}