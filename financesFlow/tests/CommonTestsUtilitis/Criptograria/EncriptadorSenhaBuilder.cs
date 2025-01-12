using DocumentFormat.OpenXml.Validation;
using financesFlow.Dominio.Seguranca.Criptografia;
using Moq;
using System.Globalization;

namespace CommonTestsUtilitis.Criptograria;
public class EncriptadorSenhaBuilder
{
    private readonly Mock<IEncriptadorSenha> _mock;

    public EncriptadorSenhaBuilder()
    {
        _mock = new Mock<IEncriptadorSenha>();
        _mock.Setup(passwordEncript => passwordEncript.Encript(It.IsAny<string>())).Returns("!%#Asknfskfn");
    }

    public EncriptadorSenhaBuilder Verify(string? password)
    {
        if(string.IsNullOrWhiteSpace(password) == false)
        {
            _mock.Setup(passwordEncript => passwordEncript.Verify(password, It.IsAny<string>())).Returns(true);
        }

        return this;
    }

    public IEncriptadorSenha Build() => _mock.Object;
}
