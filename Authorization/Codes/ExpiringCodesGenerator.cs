using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Authorization.Codes
{
    public class ExpiringCodesGenerator<T> : ICodesGenerator<T> where T : ExpiringCode, new()
    {
        private readonly ExpiringCodesGeneratorSettings _settings;

        public ExpiringCodesGenerator(IOptions<ExpiringCodesGeneratorSettings> settings)
        {
            _settings = settings.Value;
        }

        public T Generate()
        {
            using var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var bytes = new byte[_settings.Length / 2];
            cryptoServiceProvider.GetNonZeroBytes(bytes);
            return new T
            {
                Code = string.Concat(bytes.Select(b => b.ToString("x2"))),
                CreationDate = DateTime.UtcNow,
                ExpiresIn = TimeSpan.FromSeconds(_settings.ExpiresIn)
            };
        }
    }
}
