using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Exception;
using WebApi.Test.InlineData;
using FluentAssertions;
using CommonTestsUtilitis.Requests;

namespace WebApi.Test.Login
{
    public class FazLoginTest : financesFlowClassFixture
    {
        private const string METHOD = "api/Login";

        private readonly string _email;
        private readonly string _name;
        private readonly string _password;

        public FazLoginTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _email = webApplicationFactory.User_Team_Member.GetEmail();
            _name = webApplicationFactory.User_Team_Member.GetName();
            _password = webApplicationFactory.User_Team_Member.GetPassword();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginUsuarioJson
            {
                Email = _email,
                Senha = _password
            };

            var response = await DoPost(requestUri: METHOD, request: request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().Should().Be(_name);
            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Login_Invalid(string culture)
        {
            var request = RequestLoginUsuarioJsonBuilder.Build();

            var response = await DoPost(requestUri: METHOD, request: request, culture: culture);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
