

using System.Security.Cryptography;
using System.Text;

namespace VizitkaOnline.Models
{
    public static class HashModel
    {
        private static byte[] tempHash;

        public static string Sha256Hash(string inputWord)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                tempHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputWord));
            }
            foreach (var tempByte in tempHash)
            {
                stringBuilder.Append(tempByte.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
