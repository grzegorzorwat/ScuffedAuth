using BaseLibrary;
using System.Threading.Tasks;

namespace ScuffedAuth.DAL
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}
