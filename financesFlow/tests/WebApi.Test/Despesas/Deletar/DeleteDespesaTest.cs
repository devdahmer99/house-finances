﻿using System.Globalization;
using System.Net;
using System.Text.Json;
using financesFlow.Dominio.Entidades;
using financesFlow.Exception;
using FluentAssertions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Despesas.Deletar
{
    public class DeleteDespesaTest : financesFlowClassFixture
    {
    private const string METHOD = "api/Expenses";

        private readonly string _token;
        private readonly long _expenseId;

    public DeleteDespesaTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _expenseId = webApplicationFactory.Expense_MemberTeam.GetId();
    }

    [Fact]
        public async Task Success()
        {
            var result = await DoDelete(requestUri: $"{METHOD}/{_expenseId}", token: _token);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            result = await DoGet(requestUri: $"{METHOD}/{_expenseId}", token: _token);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Expense_Not_Found(string culture)
        {
            var result = await DoDelete(requestUri: $"{METHOD}/1000", token: _token, culture: culture);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
