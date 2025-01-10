using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Seguranca.Tokens;
using Microsoft.IdentityModel.Tokens;


namespace financesFlow.Infra.Seguranca.Tokens
{
    internal class JwtTokenGenerator : IGerarTokenAcesso
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _signingKey;
        public JwtTokenGenerator( uint expirationTimeMinutes, string signingKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _signingKey = signingKey;
        }
        
        public string Generate(Usuario usuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Sid, usuario.IdentificadorUsuario.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
        private SymmetricSecurityKey SecurityKey()
        {
            var key = Encoding.UTF8.GetBytes(_signingKey);
            return new SymmetricSecurityKey(key);
        }
    }
}
