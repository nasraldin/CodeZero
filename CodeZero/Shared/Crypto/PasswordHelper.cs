using CodeZero.Shared.Extensions;
using CodeZero.Shared.Extensions.Collections;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CodeZero.Shared.Crypto
{
    /// <summary>
    /// Algorithms for hash calculation
    /// </summary>
    public enum HashProvider
    {
        /// <summary>
        /// 128 bits hash, possible duplication
        /// </summary>
        MD5,

        /// <summary>
        /// 160 bits SHA hash
        /// </summary>
        SHA1,

        /// <summary>
        /// 256 bits SHA hash
        /// </summary>
        SHA256,

        /// <summary>
        /// 384 bits SHA hash
        /// </summary>
        SHA384,

        /// <summary>
        /// 512 bits SHA hash
        /// </summary>
        SHA512
    }

    /// <summary>
    /// Provide hash values to store in password, ensuring medium level security for sensitive information
    /// </summary>
    public static class PasswordHelper
    {
        private static readonly Random random = new Random((int)DateTime.Now.Ticks);
        private static readonly Dictionary<HashProvider, HashAlgorithm> hashProviders =
            new Dictionary<HashProvider, HashAlgorithm> {
                { HashProvider.MD5, MD5.Create()},
                { HashProvider.SHA1, SHA1.Create()},
                { HashProvider.SHA256, SHA256.Create()},
                { HashProvider.SHA384, SHA384.Create()},
                { HashProvider.SHA512, SHA512.Create()},
            };

        /// <summary>
        /// Generate random hash value to store against password
        /// </summary>
        /// <param name="password">String to encrypt</param>
        /// <param name="salt">Random string to salt computed hash, automatically generated if empty</param>
        /// <param name="provider">Hash algorithm to use for computing hash value</param>
        /// <returns>Hash value for the password with the addition of salt 'MD5$Salt$Hash'</returns>
        public static string GenerateHash(string password, string salt = null, HashProvider provider = HashProvider.MD5)
        {
            Check.CheckNullOrTrimEmpty(password, "Password cannot be empty");

            salt ??= GenerateSalt();

            var bytes = Encoding.Unicode.GetBytes(salt + password);
            try
            {
                var hash = hashProviders[provider].ComputeHash(bytes);
                return provider + "$" + salt + "$" + hash.ToHexString();
            }
            catch (KeyNotFoundException ex)
            {
                throw new NotSupportedException(string.Format("Hash Provider '{0}' is not supported", provider), ex);
            }
        }

        /// <summary>
        /// Validate password is equal to hashValue(Generated from Compute hash)
        /// </summary>
        /// <param name="hashValue">Computed hash value of actual password 'MD5$Salt$Hash'</param>
        /// <param name="password">Password to validate against hash value</param>
        /// <returns>True if password is equal to the hash value</returns>
        public static bool Validate(string hashValue, string password)
        {
            Check.CheckNullOrTrimEmpty(hashValue, "HashValue cannot be empty");

            var hashParts = hashValue.Split('$');
            if (hashParts.Length != 3)
                throw new ArgumentException("hashValue is not valid, it should contain hash algorithm, salt and hash value seperated by '$' e.g 'MD5$F8F25518$23C1916FF7C0A35166BEBCE564D19586'");

            HashProvider provider;
            var salt = hashParts[1];

            try { provider = hashParts[0].ToEnum<HashProvider>(); }
            catch (Exception ex) { throw new ArgumentException(string.Format("Invalid Hash Provider '{0}'", hashValue[0]), ex); }

            return hashValue == GenerateHash(password, salt, provider);
        }

        /// <summary>
        /// Random salt to comsume in hash generation
        /// </summary>
        /// <param name="length">Length of salt value should be even, hex string will be twice of the length</param>
        /// <returns>Hex string representation of salt value</returns>
        public static string GenerateSalt(int length = 4)
        {
            return GenerateSaltBytes(length).ToHexString();
        }

        /// <summary>
        /// Random salt to comsume in hash generation
        /// </summary>
        /// <param name="length">Length of salt value should be even, hex string will be twice of the length</param>
        /// <returns>bytes representation of salt value</returns>
        public static byte[] GenerateSaltBytes(int length = 16)
        {
            var salt = new byte[length];
            random.NextBytes(salt);

            return salt;
        }

        /// <summary>
        /// Generate random string to be used as passwords and salts
        /// </summary>
        /// <returns>Base 64 random string</returns>
        public static string GeneratePassword()
        {
            var randomNumber = (random.Next(5000, int.MaxValue));
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(randomNumber.ToString()));
        }
    }
}