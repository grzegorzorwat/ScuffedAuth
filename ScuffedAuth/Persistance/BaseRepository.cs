using AutoMapper;

namespace ScuffedAuth.Persistance
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        public BaseRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
