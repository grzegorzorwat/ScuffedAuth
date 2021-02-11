using Microsoft.EntityFrameworkCore;
using ScuffedAuth.DAL.Entities;
using ScuffedAuth.DAL.Mapping;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationCode = Authorization.AuthorizationCode;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

namespace ScuffedAuth.DAL.Repositories
{
    internal class AuthorizationCodesRepository : BaseRepository,
        AuthorizationEndpoint.IAuthorizationCodesRepository,
        AuthorizationCode.IAuthorizationCodesRepository
    {
        public AuthorizationCodesRepository(AppDbContext context,
            IExpressionMappingService mappingService) : base(context, mappingService) { }

        public async Task AddAuthorizationCode(AuthorizationEndpoint.AuthorizationCode authorizationCode)
        {
            var entity = _mappingService
                .MappingExpression<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity>()
                .Compile()
                .Invoke(authorizationCode);
            await _context.AuthorizationCodes.AddAsync(entity);
        }

        public async Task<AuthorizationEndpoint.Client> GetClientByIdAsync(string id)
        {
            return await _context.Clients
                .Where(x => x.Id == id)
                .Select(_mappingService.MappingExpression<ClientEntity, AuthorizationEndpoint.Client>())
                .FirstOrDefaultAsync();
        }

        public async Task<AuthorizationCode.AuthorizationCode> GetAuthorizationCode(string code)
        {
            return await _context.AuthorizationCodes
                .Where(x => x.Code == code)
                .Select(_mappingService.MappingExpression<AuthorizationCodeEntity, AuthorizationCode.AuthorizationCode>())
                .FirstOrDefaultAsync();
        }
    }
}
