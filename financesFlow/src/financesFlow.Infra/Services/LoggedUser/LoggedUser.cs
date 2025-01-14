using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Seguranca.Tokens;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private financesFlowDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;
        public LoggedUser(financesFlowDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }
        public async Task<Usuario> Get()
        {
            string token = _tokenProvider.TokenOnRequest();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            return await _dbContext.Usuarios
                .AsNoTracking()
                .FirstAsync(user => user.IdentificadorUsuario == Guid.Parse(identifier));
        }
    }
}
