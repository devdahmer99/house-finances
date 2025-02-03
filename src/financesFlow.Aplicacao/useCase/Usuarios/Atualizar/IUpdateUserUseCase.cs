using financesFlow.Comunicacao.Requests.Usuario;

namespace financesFlow.Aplicacao.useCase.Usuarios.Atualizar
{
    public interface IUpdateUserUseCase
    {
        Task Execute(RequestAtualizaUsuarioJson request);
    }
}
