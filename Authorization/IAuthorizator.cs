using System.Threading.Tasks;

namespace Authorization
{
    public interface IAuthorizator
    {
        Task<bool> Authorize();
    }
}
