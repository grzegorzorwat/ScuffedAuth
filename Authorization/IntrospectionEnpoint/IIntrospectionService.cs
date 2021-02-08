using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authorization.IntrospectionEnpoint
{
    public interface IIntrospectionService
    {
        Task<Response> Introspect(IntrospectionRequest request);
    }
}