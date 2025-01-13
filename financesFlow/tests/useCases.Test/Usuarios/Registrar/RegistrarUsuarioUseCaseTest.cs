using CommonTestsUtilitis.Criptograria;
using CommonTestsUtilitis.Mapper;
using CommonTestsUtilitis.Repositories;
using CommonTestsUtilitis.Token;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using financesFlow.Aplicacao.useCase.Usuarios.Criar;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;
using FluentAssertions;

namespace useCases.Test.Usuarios.Registrar;
public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);
        result.Should().NotBeNull();
        result.Nome.Should().Be(request.Nome);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Erro_Nome_Vazio()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Nome = string.Empty;

        var useCase = CreateUseCase();
        var acao = async () => await useCase.Execute(request);
        var result = await acao.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.NOME_VAZIO));
    }

    [Fact]
    public async Task Error_Email_Ja_Existe()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);
        var acao = async () => await useCase.Execute(request);
        var result = await acao.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.USUARIO_COM_EMAIL_JA_EXISTENTE));
    }


    private CriaUsuarioUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unidade = UnidadeDeTrabalhoBuilder.Build();
        var repositorio = RepositorioUsuarioSomenteEscrita.Build();
        var encriptador = new EncriptadorSenhaBuilder().Build();
        var gerarTokenAcesso = JwtTokenGeneratorBuilder.Builder();
        var repositorioLeitura = new RepositorioUsuarioSomenteLeituraBuilder().Build();

        if(string.IsNullOrWhiteSpace(email) == false)
        {
            repositorioLeitura.ExisteUsuarioAtivoComEmail(email);
        }

        return new CriaUsuarioUseCase(encriptador, repositorioLeitura, repositorio, unidade, mapper, gerarTokenAcesso);
    }
   
}
