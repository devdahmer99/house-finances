using Bogus;
using financesFlow.Comunicacao.Requests.Usuario;

namespace CommonTestUtilities.Requests;
public class RequestRegisterUserJsonBuilder
{
    public static RequestCriaUsuarioJson Build()
    {
        return new Faker<RequestCriaUsuarioJson>()
            .RuleFor(user => user.Nome, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Nome))
            .RuleFor(user => user.Senha, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}