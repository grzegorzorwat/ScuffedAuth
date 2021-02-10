using ScuffedAuth.DAL.Mapping;

namespace ScuffedAuth.DAL.Repositories
{
    internal abstract class BaseRepository
    {
        protected readonly AppDbContext _context;
        protected readonly IExpressionMappingService _mappingService;

        public BaseRepository(AppDbContext context, IExpressionMappingService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }
    }
}
