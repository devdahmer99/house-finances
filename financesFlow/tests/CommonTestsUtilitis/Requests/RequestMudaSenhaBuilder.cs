using Bogus;
using financesFlow.Comunicacao.Requests.Usuario;

namespace CommonTestsUtilitis.Requests
{
    public class RequestMudaSenhaBuilder
    {
        public static RequestMudaSenhaJson Build()
        {
            return new Faker<RequestMudaSenhaJson>()
                .RuleFor(user => user.Senha, faker => faker.Internet.Password())
                .RuleFor(user => user.NovaSenha, faker => faker.Internet.Password(prefix: "!Aa1"));
        }
    }
}
