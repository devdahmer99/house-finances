using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
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
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Test.Usuarios.Register
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/usuarios/registrausuario";
        private readonly HttpClient _httpClient;
        public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("nome").GetString().Should().Be(request.Nome);

            response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Nome = string.Empty;

            var useCase = CreateUseCase();

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.NOME_VAZIO));
        }

        [Fact]
        public async Task Error_Email_Already_Exist()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.EMAIL_EXISTE));
        }

        private CriaUsuarioUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnidadeDeTrabalhoBuilder.Build();
            var writeRepository = RepositorioUsuarioSomenteEscrita.Build();
            var passwordEncripter = new EncriptadorSenhaBuilder().Build();
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();
            var readRepository = new RepositorioUsuarioSomenteLeituraBuilder();

            if (string.IsNullOrWhiteSpace(email) == false)
            {
                readRepository.ExisteUsuarioAtivoComEmail(email);
            }

            return new CriaUsuarioUseCase(passwordEncripter, readRepository.Build(), writeRepository, unitOfWork, mapper ,tokenGenerator);
        }
    }
}
