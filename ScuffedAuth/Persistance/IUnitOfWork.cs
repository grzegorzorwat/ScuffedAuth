using System.Threading.Tasks;

namespace ScuffedAuth.Persistance
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
