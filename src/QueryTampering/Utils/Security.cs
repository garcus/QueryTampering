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
        private static string _hashKey = "C2CE6ACD";

        public static string CreateTamperProofQueryString(string basicQueryString)
        {
            return string.Concat(basicQueryString, "&h=", ComputeHash(basicQueryString));
        }
        public static string ComputeHash(string qs)
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
