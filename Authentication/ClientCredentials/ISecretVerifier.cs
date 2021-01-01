namespace Authentication.ClientCredentials
{
    internal interface ISecretVerifier
    {
        bool Verify(string hash, string secret);
    }
}