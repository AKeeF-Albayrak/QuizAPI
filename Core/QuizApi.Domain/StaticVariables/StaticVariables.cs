using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Domain.StaticVariables
{
    public static class StaticVariables
    {
        public static string SecureKey { get; set; } = GenerateSecureKey();
        public static string GenerateSecureKey(int size = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[size];
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }


    }
}
