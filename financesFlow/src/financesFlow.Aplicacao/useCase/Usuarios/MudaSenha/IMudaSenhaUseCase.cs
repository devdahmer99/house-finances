using financesFlow.Comunicacao.Requests.Usuario;

namespace financesFlow.Aplicacao.useCase.Usuarios.MudaSenha
{
    public interface IMudaSenhaUseCase
    {
        Task Execute(RequestMudaSenhaJson request);
    }
}
