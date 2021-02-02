using System.Security.Cryptography;
using System.Text;

namespace CodeZero.Shared.Crypto
{
    public static class Hasher
    {
        /// <summary>
        /// Supported hash algorithms
        /// </summary>
        public enum HashType
        {
            MD5, SHA1, SHA256, SHA384, SHA512
        }

        /// <summary>
        /// Gets hash of the string using a specified hash algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static byte[] GetHash(string input, HashType hash)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            return hash switch
            {
                HashType.MD5 => MD5.Create().ComputeHash(inputBytes),
                HashType.SHA1 => SHA1.Create().ComputeHash(inputBytes),
                HashType.SHA256 => SHA256.Create().ComputeHash(inputBytes),
                HashType.SHA384 => SHA384.Create().ComputeHash(inputBytes),
                HashType.SHA512 => SHA512.Create().ComputeHash(inputBytes),
                _ => inputBytes,
            };
        }

        /// <summary>
        /// Computes the hash of the string using a specified hash algorithm
        /// </summary>
        /// <param name="input">The string to hash</param>
        /// <param name="hashType">The hash algorithm to use</param>
        /// <returns>The resulting hash or an empty string on error</returns>
        public static string ComputeHash(this string input, HashType hashType)
        {
            try
            {
                byte[] hash = GetHash(input, hashType);
                StringBuilder ret = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                    ret.Append(hash[i].ToString("x2"));

                return ret.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}