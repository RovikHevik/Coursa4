

using System.Security.Cryptography;
using System.Text;

namespace VizitkaOnline.Models
{
    public class PasswordHashModel
    {
        private static byte[] tempHash;

        public string HashPassword(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                tempHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            foreach (var tempByte in tempHash)
            {
                stringBuilder.Append(tempByte.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
