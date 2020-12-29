using System;
using System.Linq;
using System.Security.Cryptography;

namespace Authentication.ClientCredentials
{
    internal class SecretVerifier : ISecretVerifier
    {
        private const int KeySize = 32;

        public bool Verify(string hash, string secret)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format.");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            using var algorithm = new Rfc2898DeriveBytes(secret,
                salt,
                iterations,
                HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(KeySize);
            return keyToCheck.SequenceEqual(key);
        }
    }
}
