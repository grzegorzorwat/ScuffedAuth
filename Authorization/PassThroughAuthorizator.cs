using System.Threading.Tasks;

namespace Authorization
{
    internal class PassThroughAuthorizator : IAuthorizator
    {
        public Task<bool> Authorize()
        {
            return Task.FromResult(true);
        }
    }
}
