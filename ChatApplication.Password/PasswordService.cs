using System;
using System.Security.Cryptography;
using System.Text;
using ChatApplication.Data.Contracts;
using ChatApplication.Security.Contracts;

namespace ChatApplication.Security
{
    public class PasswordService : IPasswordService
    {
        private readonly int _saltLength;
        private readonly int _pwdLength;
        private readonly int _iterations;
        private readonly HMACSHA256 _hmacsha256;
        private readonly RNGCryptoServiceProvider _crypto;

        public PasswordService(IApplicationSettings settings, RNGCryptoServiceProvider crypto)
        {
            _saltLength = int.Parse(settings.GetValue("PWD:SaltLength"));
            _pwdLength = int.Parse(settings.GetValue("PWD:Length"));
            _iterations = int.Parse(settings.GetValue("PWD:Iterations"));

            _crypto = crypto;

            var key = settings.GetValue("PWD:Key");
            _hmacsha256 = new HMACSHA256(Encoding.ASCII.GetBytes(key));
        }

        public string GeneratePasswordHash(string password)
        {
            var salt = Salt;
            return Convert.ToBase64String(Combine(salt, Hash(password, salt)));
        }

        public bool ValidatePassword(string hash, string password)
        {
            var split = SplitHash(Convert.FromBase64String(hash));
            var source = split.Item1;
            var compareTo = Hash(password, split.Item2);
            for (var i = 0; i < split.Item2.Length; ++i)
            {
                if (source[i] != compareTo[i]) return false;
            }
            return true;
        }

        private byte[] Keyed(byte[] password)
        {
            return _hmacsha256.ComputeHash(password);
        }

        private Tuple<byte[], byte[]> SplitHash(byte[] hash)
        {
            var salt = new byte[_saltLength];
            var pwdLength = hash.Length - _saltLength;
            var password = new byte[pwdLength];
            Array.Copy(hash, 0, salt, 0, _saltLength);
            Array.Copy(hash, _saltLength, password, 0, pwdLength);
            return new Tuple<byte[], byte[]>(password, salt);
        }

        private byte[] Hash(string password, byte[] salt)
        {
            var pdkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations);
            return Keyed(pdkdf2.GetBytes(_pwdLength));
        }

        private static byte[] Combine(byte[] salt, byte[] password)
        {
            var hash = new byte[salt.Length + password.Length];
            Array.Copy(salt, 0, hash, 0, salt.Length);
            Array.Copy(password, 0, hash, salt.Length, password.Length);
            return hash;
        }

        private byte[] Salt
        {
            get
            {
                byte[] salt;
                _crypto.GetBytes(salt = new byte[_saltLength]);
                return salt;
            }
        }
    }
}
