using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Services.LoggedUser;
using Moq;

namespace CommonTestsUtilitis.Login
{
    public class LoggedUserBuilder
    {
        public static ILoggedUser Build(Usuario user)
        {
            var mock = new Mock<ILoggedUser>();
            mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);
            return mock.Object;
        }
    }
}
