using financesFlow.Comunicacao.Responses.Usuario;

namespace financesFlow.Aplicacao.useCase.Usuarios.Profile
{
    public interface IGetUserProfileUseCase
    {
        Task<ResponseUserProfileJson> Execute();
    }
}
