using System;
using System.Text;

namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientCredentialsDecoder
    {
        private const string BasicHeaderPrefix = "Basic ";

        private string _value = string.Empty;
        private bool _canContinue = true;
        private ClientCredentialsModel _credentials = ClientCredentialsModel.Empty;

        public bool TryDecode(string authorizationHeader, out ClientCredentialsModel credentials)
        {
            SetUp(authorizationHeader);
            Decode();
            credentials = _credentials;
            return _canContinue;
        }

        private void SetUp(string authorizationHeader)
        {
            _value = authorizationHeader;
            _canContinue = true;
            _credentials = ClientCredentialsModel.Empty;
        }

        private void Decode()
        {
            RemoveBasicHeaderPrefix();
            DecodeFromBase64String();
            ExtractCredentials();
        }

        private void RemoveBasicHeaderPrefix()
        {
            if (!_canContinue)
            {
                return;
            }

            if (!_value.StartsWith(BasicHeaderPrefix))
            {
                _canContinue = false;
                return;
            }

            _value = _value.Remove(0, BasicHeaderPrefix.Length);
        }

        private void DecodeFromBase64String()
        {
            if (!_canContinue)
            {
                return;
            }

            var buffer = new Span<byte>(new byte[_value.Length]);

            if (!Convert.TryFromBase64String(_value, buffer, out int bytesParsed))
            {
                _canContinue = false;
                return;
            }

            _value = Encoding.ASCII.GetString(buffer.Slice(0, bytesParsed));
        }

        private void ExtractCredentials()
        {
            if (!_canContinue)
            {
                return;
            }

            int separatorIndex = _value.IndexOf(':');

            if (separatorIndex == -1)
            {
                _canContinue = false;
                return;
            }

            _credentials = new ClientCredentialsModel(_value[..separatorIndex],
                _value[(separatorIndex + 1)..]);
        }
    }
}
