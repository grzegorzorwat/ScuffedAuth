using AutoMapper;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;
using System.Threading.Tasks;
using AuthorizationCode = Authorization.AuthorizationCode;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

namespace ScuffedAuth.DAL
{
    internal class AuthorizationCodesRepository : BaseRepository,
        AuthorizationEndpoint.IAuthorizationCodesRepository,
        AuthorizationCode.IAuthorizationCodesRepository
    {
        private readonly IMapper<ClientEntity, AuthorizationEndpoint.Client> _authorizationEndpointClientMapper;

        public AuthorizationCodesRepository(AppDbContext context,
            IMapper mapper,
            IMapper<ClientEntity, AuthorizationEndpoint.Client> authorizationEndpointClientMapper) : base(context, mapper)
        {
            _authorizationEndpointClientMapper = authorizationEndpointClientMapper;
        }

        public async Task AddAuthorizationCode(AuthorizationEndpoint.AuthorizationCode authorizationCode)
        {
            var entity = _mapper.Map<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity>(authorizationCode);
            await _context.AuthorizationCodes.AddAsync(entity);
        }

        public async Task<AuthorizationEndpoint.Client> GetClientByIdAsync(string id)
        {
            var clientEntity = await _context.Clients.FindAsync(id);
            return _authorizationEndpointClientMapper.Map(clientEntity);
        }

        public async Task<AuthorizationCode.AuthorizationCode> GetAuthorizationCode(string code)
        {
            var entity = await _context.AuthorizationCodes.FindAsync(code);
            return _mapper.Map<AuthorizationCodeEntity, AuthorizationCode.AuthorizationCode>(entity);
        }
    }
}
