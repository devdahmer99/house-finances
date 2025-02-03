using financesFlow.Dominio.Entidades;

namespace financesFlow.Aplicacao.useCase.Usuario.Criar
{
    public interface ICriaUsuarioUseCase
    {
        Task Execute(Usuario usuario);
    }
}
