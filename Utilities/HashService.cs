using Buscador.Core.Interfaces;
using Buscador.Core.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace Buscador.Utilities
{
    public class HashService : IHashService
    {
        private readonly IPasswordHasher<object> _passwordHasher;
        private readonly SecuritySettings _settings;

        public HashService(IPasswordHasher<object> passwordHasher,
        IOptions<SecuritySettings> settings)
        {
            _passwordHasher = passwordHasher;
            _settings = settings.Value;
        }

        public string EncriptPassword(string password)
        {
            var encriptedPassword = password + _settings.SecretKey;
            encriptedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(encriptedPassword));

            return encriptedPassword;
        }

        public string DecriptPassword(string password)
        {
            var encriptedPassword = password + _passwordHasher;
            string base64Codificado = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(password));

            return encriptedPassword;
        }

        public string VerifyPassword(string userPassword, string loginPassword)
        {
            var encryptedPassword = EncriptPassword(loginPassword);
            return encryptedPassword == userPassword ? "Valid" : "Invalid";
        }
    }
}
