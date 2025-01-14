using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Usuarios;
using Moq;

namespace CommonTestsUtilitis.Repositories;
public class RepositorioUsuarioSomenteLeituraBuilder
{
    private readonly Mock<IRepositorioUsuarioSomenteLeitura> _repositorio;
    public RepositorioUsuarioSomenteLeituraBuilder()
    {
        _repositorio = new Mock<IRepositorioUsuarioSomenteLeitura>();
    }
    public IRepositorioUsuarioSomenteLeitura Build() => _repositorio.Object;

    public void ExisteUsuarioAtivoComEmail(string email)
    {
        _repositorio.Setup(userReadOnly => userReadOnly.ExisteUsuarioAtivoComEmail(email)).ReturnsAsync(true);
    }

    public RepositorioUsuarioSomenteLeituraBuilder BuscaUsuarioPorEmail(Usuario user)
    {
        _repositorio.Setup(userRepository => userRepository.BuscaUsuarioPorEmail(user.Email)).ReturnsAsync(user);

        return this;
    }
}
