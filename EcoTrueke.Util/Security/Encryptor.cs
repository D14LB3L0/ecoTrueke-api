using System.Security.Cryptography;
using System.Text;

namespace EcoTrueke.Util.Security
{
    public static class Encryptor
    {
        public static string SHA256Hash(string text)
        {
            using (SHA256 objSHA256 = SHA256.Create())
            {
                byte[] bytes = objSHA256.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder objStringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    objStringBuilder.Append(bytes[i].ToString("x2"));
                return objStringBuilder.ToString();
            }
        }
    }
}
