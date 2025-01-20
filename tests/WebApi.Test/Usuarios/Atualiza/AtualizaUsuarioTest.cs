using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using CommonTestsUtilitis.Requests;
using financesFlow.Exception;
using WebApi.Test.InlineData;
using FluentAssertions;

namespace WebApi.Test.Usuarios.Atualiza
{
    public class AtualizaUsuarioTest : financesFlowClassFixture
    {
        private const string METHOD = "api/User";

        private readonly string _token;

        public AtualizaUsuarioTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.User_Team_Member.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestAtualizaUsuarioBuilder.Build();

            var response = await DoPut(METHOD, request, token: _token);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestAtualizaUsuarioBuilder.Build();
            request.Nome = string.Empty;

            var response = await DoPut(METHOD, request, token: _token, culture: culture);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
