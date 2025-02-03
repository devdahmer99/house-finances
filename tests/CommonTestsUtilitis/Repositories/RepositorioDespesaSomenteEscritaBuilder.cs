using financesFlow.Dominio.Repositories.Despesas;
using Moq;

namespace CommonTestsUtilitis.Repositories
{
    public class RepositorioDespesaSomenteEscritaBuilder
    {
        public static IRepositorioDepesaSomenteEscrita Build()
        {
            var mock = new Mock<IRepositorioDepesaSomenteEscrita>();

            return mock.Object;
        }
    }
}
