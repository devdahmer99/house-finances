using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Seguranca.Tokens;
using Moq;

namespace CommonTestsUtilitis.Token;
public class JwtTokenGeneratorBuilder
{
    public static IGerarTokenAcesso Builder()
    {
        var mock = new Mock<IGerarTokenAcesso>();

        mock.Setup(config => config.Generate(It.IsAny<Usuario>())).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        return mock.Object;
    }
}
