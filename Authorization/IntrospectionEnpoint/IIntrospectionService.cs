using System.Threading.Tasks;

namespace Authorization.IntrospectionEnpoint
{
    public interface IIntrospectionService
    {
        Task<IntrospectionResponse> Introspect(IntrospectionRequest request);
    }
}