using Authorization.AuthorizationEndpoint;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;
using System.Threading.Tasks;

namespace ScuffedAuth.Persistance
{
    public class AuthorizationCodesRepository : BaseRepository, IAuthorizationCodesRepository
    {
        private readonly IMapper _mapper;

        public AuthorizationCodesRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task AddAuthorizationCode(AuthorizationCode authorizationCode)
        {
            var entity = _mapper.Map<AuthorizationCode, AuthorizationCodeEntity>(authorizationCode);
            await _context.AuthorizationCodes.AddAsync(entity);
        }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            var clientEntity = await _context.Clients.FindAsync(id);
            return _mapper.Map<ClientEntity, Client>(clientEntity);
        }
    }
}
