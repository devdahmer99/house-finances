using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses.Usuario;
using FluentAssertions;

namespace WebApi.Test.Usuarios.DoLogin
{
    public class FazLoginUsuarioTest : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/login/efetualogin";
        private readonly HttpClient _httpClient;
        private readonly string _email;
        private readonly string _nome;
        private readonly string _senha;
        public FazLoginUsuarioTest(CustomWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
            _nome = webApplicationFactory.getNome();
            _email = webApplicationFactory.getEmail();
            _senha = webApplicationFactory.getPassword();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginUsuarioJson
            {
                Email = _email,
                Senha = _senha
            };

            var response = await _httpClient.PostAsJsonAsync(METHOD, request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("nome").GetString().Should().Be(_nome);
            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }
    }
}
