using financesFlow.Aplicacao.useCase.Usuarios;
using financesFlow.Comunicacao.Requests.Usuario;
using FluentAssertions;
using FluentValidation;

namespace ValidatorsTests.Usuarios
{
    public class PasswordValidatorTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        [InlineData("Aaaaaaaa")]
        [InlineData("Aaaaaaa1")]
        public void Erro_Senha_Invalida(string password)
        {
            var validator = new ValidacaoSenha<RequestCriaUsuarioJson>();

            var result = validator
                .IsValid(new ValidationContext<RequestCriaUsuarioJson>(new RequestCriaUsuarioJson()), password);

            result.Should().BeFalse();
        }
    }
}
