using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Seguranca.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace financesFlow.Infra.Seguranca.Tokens
{
    internal class JwtTokenGenerator : IGerarTokenAcesso
    {
        private readonly uint _tempoExpiracaoMinutos;
        private readonly string _chaveEntrada;
        public JwtTokenGenerator(IOptions<JwtTokenSettings> jwtsettings)
        {
            _tempoExpiracaoMinutos = jwtsettings.Value.ExpiresMinutes;
            _chaveEntrada = jwtsettings.Value.SigningKey!;
        }
        
        public string GerarTokenAcesso(Usuario usuario)
        {
            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Sid, usuario.IdentificadorUsuario.ToString()),
            new Claim(ClaimTypes.Role, usuario.Permissao)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_tempoExpiracaoMinutos),
                SigningCredentials = new SigningCredentials(ChaveSeguranca(), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
        private SymmetricSecurityKey ChaveSeguranca()
        {
            Console.WriteLine(_chaveEntrada);
            var chave = Encoding.UTF8.GetBytes(_chaveEntrada);
            return new SymmetricSecurityKey(chave);
        }
    }
}
