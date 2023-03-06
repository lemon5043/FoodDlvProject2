using System.Security.Cryptography;
using System.Text;

namespace FoodDlvAPI.Infrastructures
{
    public static class HashUtility
    {
        public static string ToSHA256(string plainText, string salt)
        {
            // ref https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha256?view=net-6.0
            using (SHA256 mySHA256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(salt + plainText);
                var hash = mySHA256.ComputeHash(passwordBytes);
                StringBuilder sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        public static void ToHMACSHA512(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {          
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));               
            }
        }
    }
}
