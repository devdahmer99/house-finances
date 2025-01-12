using Bogus;
using financesFlow.Comunicacao.Requests.Usuario;

namespace CommonTestsUtilitis.Requests;
public class RequestLoginUsuarioJsonBuilder
{
    public static RequestLoginUsuarioJson Build()
    {
        return new Faker<RequestLoginUsuarioJson>()
                    .RuleFor(user => user.Email, faker => faker.Internet.Email())
                    .RuleFor(user => user.Senha, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}
