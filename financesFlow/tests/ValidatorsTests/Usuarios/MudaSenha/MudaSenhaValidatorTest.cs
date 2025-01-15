using CommonTestsUtilitis.Requests;
using financesFlow.Aplicacao.useCase.Usuarios.MudaSenha;
using financesFlow.Exception;
using FluentAssertions;

namespace ValidatorsTests.Usuarios.MudaSenha
{
    public class MudaSenhaValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new MudaSenhaValidator();

            var request = RequestMudaSenhaBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_NewPassword_Empty(string newPassword)
        {
            var validator = new MudaSenhaValidator();

            var request = RequestMudaSenhaBuilder.Build();
            request.NovaSenha = newPassword;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.SENHA_INVALIDA));
        }
    }
}
