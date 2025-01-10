using CommonTestUtilities.Requests;
using financesFlow.Aplicacao.useCase.Usuarios.Criar;
using financesFlow.Exception;
using FluentAssertions;

namespace ValidatorsTests.Usuarios.Registrar
{
    public class RegistrarUsuarioValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ValidacaoRegistrarUsuario();
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void Erro_Nome_Invalido(string nome)
        {
            var validator = new ValidacaoRegistrarUsuario();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Nome = nome;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.NOME_VAZIO));
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void Erro_Email_Vazio(string email)
        {
            var validator = new ValidacaoRegistrarUsuario();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = email;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.USUARIO_COM_EMAIL_VAZIO));
        }
    }
}
