using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.IntrospectionEnpoint
{
    public interface IIntrospectionService
    {
        Task<IntrospectionResponse> Introspect(IntrospectionRequest request);
    }
}