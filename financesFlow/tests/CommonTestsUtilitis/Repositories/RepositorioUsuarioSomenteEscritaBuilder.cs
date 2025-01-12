using financesFlow.Dominio.Repositories.Usuarios;
using Moq;

namespace CommonTestUtilities.Repositories;
public class RepositorioUsuarioSomenteEscrita
{
    public static IRepositorioUsuarioSomenteEscrita Build()
    {
        var mock = new Mock<IRepositorioUsuarioSomenteEscrita>();

        return mock.Object;
    }
}