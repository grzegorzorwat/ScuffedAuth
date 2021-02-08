using System.Threading.Tasks;

namespace BaseLibrary
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
