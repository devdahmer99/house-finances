using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses.Usuario;

namespace financesFlow.Aplicacao.useCase.Usuarios.Criar
{
    public interface ICriarUsuarioUseCase
    {
        Task<ResponseCriaUsuarioJson> Execute(RequestCriaUsuarioJson requestUsuario);
    }
}
