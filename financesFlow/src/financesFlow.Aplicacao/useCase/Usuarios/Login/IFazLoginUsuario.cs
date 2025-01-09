using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses.Usuario;

namespace financesFlow.Aplicacao.useCase.Usuarios.Login
{
    public interface IFazLoginUsuario
    {
        Task<ResponseUsuarioRegistradoJson> Execute(RequestLoginUsuarioJson requestLogin);
    }
}
