using AutoMapper;
using ScuffedAuth.Persistance.Entities;
using System.Threading.Tasks;
using AuthorizationCode = Authentication.AuthorizationCode;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

namespace ScuffedAuth.Persistance
{
    public class AuthorizationCodesRepository : BaseRepository, AuthorizationEndpoint.IAuthorizationCodesRepository, AuthorizationCode.IAuthorizationCodesRepository
    {
        public AuthorizationCodesRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task AddAuthorizationCode(AuthorizationEndpoint.AuthorizationCode authorizationCode)
        {
            var entity = _mapper.Map<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity>(authorizationCode);
            await _context.AuthorizationCodes.AddAsync(entity);
        }

        public async Task<AuthorizationEndpoint.Client> GetClientByIdAsync(string id)
        {
            var clientEntity = await _context.Clients.FindAsync(id);
            return _mapper.Map<ClientEntity, AuthorizationEndpoint.Client>(clientEntity);
        }

        public async Task<AuthorizationCode.AuthorizationCode?> GetAuthorizationCode(string code)
        {
            var entity = await _context.AuthorizationCodes.FindAsync(code);
            return _mapper.Map<AuthorizationCodeEntity, AuthorizationCode.AuthorizationCode>(entity);
        }
    }
}
