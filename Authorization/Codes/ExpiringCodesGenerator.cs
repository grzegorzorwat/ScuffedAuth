using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Authorization.Codes
{
    public abstract class ExpiringCodesGenerator
    {
        private readonly ExpiringCodesGeneratorSettings _settings;

        public ExpiringCodesGenerator(IOptions<ExpiringCodesGeneratorSettings> settings)
        {
            _settings = settings.Value;
        }

        protected string GenerateCode()
        {
            using var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var bytes = new byte[_settings.Length / 2];
            cryptoServiceProvider.GetNonZeroBytes(bytes);
            return string.Concat(bytes.Select(b => b.ToString("x2")));
        }

        protected DateTime GetCreationDate()
        {
            return DateTime.UtcNow;
        }

        protected TimeSpan GetExpiresIn()
        {
            return TimeSpan.FromSeconds(_settings.ExpiresIn);
        }
    }
}
