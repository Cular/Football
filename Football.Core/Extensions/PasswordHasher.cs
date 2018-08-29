using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Football.Core.Extensions
{
    public static class PasswordHasher
    {
        private static readonly byte[] salt;

        static PasswordHasher()
        {
            var configs = new ConfigurationBuilder()
                .AddJsonFile("settings.json")
                .Build();

            salt = Encoding.UTF8.GetBytes(configs["PasswordSalt"]);
        }

        public static string GetPasswordHash(string password)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[28];
            Array.Copy(salt, 0, hashBytes, 0, 8);
            Array.Copy(hash, 0, hashBytes, 8, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VeifyPassword(string password, string passwordhash)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordhash);

            Array.Copy(hashBytes, 0, salt, 0, 8);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);
            
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 8] != hash[i])
                    return false;

            return true;
        }
    }
}
