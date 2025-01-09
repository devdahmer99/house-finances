using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses.Usuario;
using financesFlow.Dominio.Repositories.Usuarios;
using financesFlow.Dominio.Seguranca.Criptografia;
using financesFlow.Dominio.Seguranca.Tokens;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Usuarios.Login
{
    public class LoginUsuarioUseCase : IFazLoginUsuario
    {
        private readonly IRepositorioUsuarioSomenteLeitura _repositorioLeitura;
        private readonly IEncriptadorSenha _encriptador;
        private readonly IGerarTokenAcesso _gerarTokenAcesso;

        public LoginUsuarioUseCase(IRepositorioUsuarioSomenteLeitura repositorioLeitura,
           IEncriptadorSenha encriptador, IGerarTokenAcesso gerarTokenAcesso)
        {
            _repositorioLeitura = repositorioLeitura;
            _encriptador = encriptador;
            _gerarTokenAcesso = gerarTokenAcesso;
        }

        public async Task<ResponseUsuarioRegistradoJson> Execute(RequestLoginUsuarioJson requestLogin)
        {
            var usuario = await _repositorioLeitura.BuscaUsuarioPorEmail(requestLogin.Email);
            if(usuario == null)
            {
                throw new LoginInvalidException();
            }

            var senhaEncontrada = _encriptador.VerificaSenha(requestLogin.Senha, usuario.Senha);

            if(senhaEncontrada == false)
            {
                throw new LoginInvalidException();
            }

            return new ResponseUsuarioRegistradoJson
            {
                Nome = usuario.Nome,
                Token = _gerarTokenAcesso.GerarTokenAcesso(usuario)
            };
        }
    }
}
