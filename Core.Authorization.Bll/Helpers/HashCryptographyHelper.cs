using System;
using System.Security.Cryptography;
using System.Text;
using Core.Authorization.Bll.Abstract;

namespace Core.Authorization.Bll.Helpers
{
    public class HashCryptographyHelper : IHashCryptographyHelper
    {
        public string GetMd5Hash(string input)

        {
            // PLEASE DO NOT USE MD5!!!!
            // Create a new instance of the MD5CryptoServiceProvider object.
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hasher.ComputeHash(Encoding.GetEncoding(0).GetBytes(input));

            return ConvertByteToString(data);
        }

        public string GetSha256Hash(string input)
        {
            var hasher = SHA256.Create();
            var data = hasher.ComputeHash(Encoding.GetEncoding(0).GetBytes(input));

            return ConvertByteToString(data);
        }

        public string GetSha512Hash(string input)
        {
            var hasher = SHA512.Create();
            var data = hasher.ComputeHash(Encoding.GetEncoding(0).GetBytes(input));

            return ConvertByteToString(data);
        }

        public string GetSalt()
        {
            var bytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return GetSha512Hash(BitConverter.ToString(bytes).Replace("-", ""));
        }

        public string GetSaltPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password)) return password;
            var result = new StringBuilder();

            for (int p = 0, s = 0; p < password.Length; p++, s++)
            {
                if (s >= salt.Length)
                    s = 0;
                result.Append((char)(password[p] ^ (uint)salt[s]));
            }
            return GetSha512Hash(result.ToString());
        }

        public string GetPassword(int length = 4)
        {
            var password = new byte[length];

            using (var rngCsp = RandomNumberGenerator.Create())
            {
                rngCsp.GetBytes(password);
            }

            var code = BitConverter.ToString(password).Replace("-", "");
            return code;
        }

        private static string ConvertByteToString(byte[] data)
        {
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
