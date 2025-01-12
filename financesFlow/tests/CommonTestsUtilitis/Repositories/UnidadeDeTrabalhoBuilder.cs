using financesFlow.Dominio.Repositories;
using Moq;

namespace CommonTestsUtilitis.Repositories;
public class UnidadeDeTrabalhoBuilder
{
    public static IUnidadeDeTrabalho Build()
    {
        var mock = new Mock<IUnidadeDeTrabalho>();

        return mock.Object;
    }
}
