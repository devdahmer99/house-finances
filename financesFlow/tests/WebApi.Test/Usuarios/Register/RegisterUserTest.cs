﻿using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestUtilities.Requests;
using financesFlow.Exception;
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
        public async Task Error_Empty_Name()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Nome = string.Empty;

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("ErrorMessages").EnumerateArray();

            errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorMessages.NOME_VAZIO));
        }
    }
}
