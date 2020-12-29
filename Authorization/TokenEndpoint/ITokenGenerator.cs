namespace Authorization.TokenEndpoint
{
    public interface ITokenGenerator
    {
        Token Generate();
    }
}
