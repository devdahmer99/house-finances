using Bogus;
using CommonTestsUtilitis.Criptograria;
using financesFlow.Dominio.Entidades;

namespace CommonTestsUtilitis.Entidades;
public class UserBuilder
{
    public static Usuario Build()
    {
        var encriptadorSenha = new EncriptadorSenhaBuilder().Build();

        var usuario = new Faker<Usuario>()
            .RuleFor(user => user.Id, _ => 1)
            .RuleFor(user => user.Nome, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, usuario) => faker.Internet.Email(usuario.Nome))
            .RuleFor(user => user.Senha, (_, usuario) => encriptadorSenha.Encript(usuario.Senha))
            .RuleFor(user => user.IdentificadorUsuario, _ => Guid.NewGuid());

        return usuario;
    }
}
