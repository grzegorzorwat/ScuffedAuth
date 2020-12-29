namespace Authorization
{
    public interface IAuthorizationFactory
    {
        IAuthorization GetAuthorization(GrantTypes grantType);
    }
}