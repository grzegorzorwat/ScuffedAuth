namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public interface ITokenGenerator
    {
        Token Generate();
    }
}
