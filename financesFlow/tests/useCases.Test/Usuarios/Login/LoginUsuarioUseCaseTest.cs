using CommonTestsUtilitis.Criptograria;
using CommonTestsUtilitis.Entidades;
using CommonTestsUtilitis.Repositories;
using CommonTestsUtilitis.Requests;
using CommonTestsUtilitis.Token;
using financesFlow.Aplicacao.useCase.Usuarios.Login;
using financesFlow.Dominio.Entidades;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.Usuarios.Login;
public class LoginUsuarioUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginUsuarioJsonBuilder.Build();
        var useCase = CreateUseCase(user, request.Senha);
        var acao = async () => await useCase.Execute(request);
        var result = await acao.Should().ThrowAsync<LoginInvalidException>();
        result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS));
    }

    [Fact]
    public async Task Erro_Senha_Nao_Bate()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginUsuarioJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user);
        var acao = async () => await useCase.Execute(request);
        var result = await acao.Should().ThrowAsync<LoginInvalidException>();
        result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS));
    }

    private LoginUsuarioUseCase CreateUseCase(Usuario user, string? password = null)
    {
        var repositorioLeitura = new RepositorioUsuarioSomenteLeituraBuilder().BuscaUsuarioPorEmail(user).Build();
        var encritador = new EncriptadorSenhaBuilder().Verify(password).Build();
        var gerarTokenAcesso = JwtTokenGeneratorBuilder.Builder();

        return new LoginUsuarioUseCase(repositorioLeitura, encritador, gerarTokenAcesso);
    }
}
