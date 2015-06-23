using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QueryTampering.Utils
{
    public static class Security
    {
        public static string ComputeHash(string qs, string _hashKey)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(qs);
            HMACSHA1 hashAlgorithm = new HMACSHA1(Security.HexToByteArray(_hashKey));
            byte[] hash = hashAlgorithm.ComputeHash(textBytes);
            return Security.ByteArrayToHex(hash);
        }

        public static byte[] HexToByteArray(string hexString)
        {
            if (0 != (hexString.Length % 2))
            {
                throw new Exception("Hex string must be multiple of 2 in length");
            }

            int byteCount = hexString.Length / 2;
            byte[] byteValues = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                byteValues[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return byteValues;
        }

        public static string ByteArrayToHex(byte[] data)
        {
            return BitConverter.ToString(data);
        }
    }
}
