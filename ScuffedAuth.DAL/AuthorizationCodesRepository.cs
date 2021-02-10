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
        private readonly IMapper<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity> _authorizationCodeEntityMapper;
        private readonly IMapper<AuthorizationCodeEntity, AuthorizationCode.AuthorizationCode> _authorizationCodeMapper;

        public AuthorizationCodesRepository(AppDbContext context,
            IMapper<ClientEntity, AuthorizationEndpoint.Client> authorizationEndpointClientMapper,
            IMapper<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity> authorizationCodeEntityMapper,
            IMapper<AuthorizationCodeEntity, AuthorizationCode.AuthorizationCode> authorizationCodeMapper) : base(context)
        {
            _authorizationEndpointClientMapper = authorizationEndpointClientMapper;
            _authorizationCodeEntityMapper = authorizationCodeEntityMapper;
            _authorizationCodeMapper = authorizationCodeMapper;
        }

        public async Task AddAuthorizationCode(AuthorizationEndpoint.AuthorizationCode authorizationCode)
        {
            var entity = _authorizationCodeEntityMapper.Map(authorizationCode);
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
            return _authorizationCodeMapper.Map(entity);
        }
    }
}
