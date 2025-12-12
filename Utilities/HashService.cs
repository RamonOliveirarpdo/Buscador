using Buscador.Core.Interfaces;
using Buscador.Core.Settings;
using Microsoft.Extensions.Options;

namespace Buscador.Utilities
{
    public class HashService : IHashService
    {
        private readonly string _encryptionKey;

        public HashService(IOptions<SecuritySettings> settings)
        {
            _encryptionKey = settings.Value.EncryptionKey;
        }

        public string HashPassword(string password)
        {
           var encriptedPassword = password + _encryptionKey;
           encriptedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(encriptedPassword));

            return encriptedPassword;
        }
    }
}
