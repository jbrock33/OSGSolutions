using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OSGSolutions.Services
{
    public class CryptographyService
    {
        public string GenerateRandomString(int length)
        {
            byte[] bytes = new byte[(int)Math.Floor(length * .75)];
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

        public string Hash(string password, string salt)
        {
            string key = salt;
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash;
            using (HMACSHA256 hasher = new HMACSHA256(keyBytes))
            {
                hash = hasher.ComputeHash(messageBytes);
            }
            
            return Convert.ToBase64String(hash);
        }
    }
}