﻿using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using financesFlow.Exception;
using WebApi.Test.InlineData;
using FluentAssertions;
using CommonTestsUtilitis.Requests;
using financesFlow.Comunicacao.Requests.Usuario;

namespace WebApi.Test.Usuarios.MudaSenha
{
    public class MudaSenhaTest : financesFlowClassFixture
    {
        private const string METHOD = "api/User/change-password";

        private readonly string _token;
        private readonly string _password;
        private readonly string _email;

        public MudaSenhaTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.User_Team_Member.GetToken();
            _password = webApplicationFactory.User_Team_Member.GetPassword();
            _email = webApplicationFactory.User_Team_Member.GetEmail();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestMudaSenhaBuilder.Build();
            request.Senha = _password;

            var response = await DoPut(METHOD, request, _token);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var loginRequest = new RequestLoginUsuarioJson
            {
                Email = _email,
                Senha = _password,
            };

            response = await DoPost("api/Login", loginRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            loginRequest.Senha = request.NovaSenha;

            response = await DoPost("api/Login", loginRequest);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Password_Different_Current_Password(string culture)
        {
            var request = RequestMudaSenhaBuilder.Build();

            var response = await DoPut(METHOD, request, token: _token, culture: culture);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("PASSWORD_DIFFERENT_CURRENT_PASSWORD", new CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
