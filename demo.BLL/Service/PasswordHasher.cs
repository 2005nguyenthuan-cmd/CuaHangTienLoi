using System;
using System.Security.Cryptography;

namespace demo.BLL.Service
{
    public static class PasswordHasher
    {
        private const string Prefix = "PBKDF2-SHA256";
        private const int Iterations = 100000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = DeriveHash(password, salt, Iterations);

            return string.Join("$",
                Prefix,
                Iterations.ToString(),
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        public static bool VerifyPassword(string password, string storedPassword)
        {
            if (password == null || string.IsNullOrWhiteSpace(storedPassword))
            {
                return false;
            }

            if (!IsHashedPassword(storedPassword))
            {
                return string.Equals(password, storedPassword, StringComparison.Ordinal);
            }

            string[] parts = storedPassword.Split('$');
            if (parts.Length != 4 || !int.TryParse(parts[1], out int iterations))
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expectedHash = Convert.FromBase64String(parts[3]);
                byte[] actualHash = DeriveHash(password, salt, iterations);

                return FixedTimeEquals(actualHash, expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsHashedPassword(string storedPassword)
        {
            return storedPassword != null &&
                   storedPassword.StartsWith(Prefix + "$", StringComparison.Ordinal);
        }

        private static byte[] DeriveHash(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
            {
                return false;
            }

            int difference = 0;
            for (int i = 0; i < left.Length; i++)
            {
                difference |= left[i] ^ right[i];
            }

            return difference == 0;
        }
    }
}
