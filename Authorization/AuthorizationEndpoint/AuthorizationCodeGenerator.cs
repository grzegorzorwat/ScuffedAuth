namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCodeGenerator : IAuthorizationCodeGenerator
    {
        private readonly ICodesGenerator<AuthorizationCode> _codesGenerator;

        public AuthorizationCodeGenerator(ICodesGenerator<AuthorizationCode> codesGenerator)
        {
            _codesGenerator = codesGenerator;
        }

        public AuthorizationCode Generate(string clientId, string redirectionUri)
        {
            var code = _codesGenerator.Generate();
            code.ClientId = clientId;
            code.RedirectUri = redirectionUri;
            return code;
        }
    }
}
