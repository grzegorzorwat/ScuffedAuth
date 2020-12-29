namespace Authentication.ClientCredentials
{
    public interface ISecretVerifier
    {
        bool Verify(string hash, string secret);
    }
}