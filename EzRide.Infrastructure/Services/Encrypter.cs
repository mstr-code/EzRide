using System;
using System.Security.Cryptography;

using EzRide.Infrastructure.Extensions;

namespace EzRide.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int deriveBytesInterationsCount = 10000;
        private static readonly int saltSize = 40;

        public string GetSalt()
        {   
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] saltBytes = new byte[saltSize];
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (value.Empty())
                throw new ArgumentException("Cannot generate hash from an empty value.",
                    nameof(value));

            if (salt.Empty())
                throw new ArgumentException("Cannot use an empty salt from hashing value.",
                    nameof(value));
            
            var pbkdf2 =
                new Rfc2898DeriveBytes(value, GetBytes(salt), deriveBytesInterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(saltSize));
        }

        private static byte[] GetBytes(string value)
        {
            byte[] bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}