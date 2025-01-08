using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Exception;
using FluentValidation;

namespace financesFlow.Aplicacao.useCase.Usuarios
{
    public class ValidacaoUsuario : AbstractValidator<RequestCriaUsuarioJson>
    {
        public ValidacaoUsuario()
        {
            RuleFor(usu => usu.Nome).NotEmpty()
                .WithMessage(ResourceErrorMessages.USUARIO_COM_NOME_VAZIO);
            RuleFor(usu=> usu.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.USUARIO_COM_EMAIL_VAZIO)
                .EmailAddress()
                .WithMessage(ResourceErrorMessages.USUARIO_COM_EMAIL_INVALIDO);
            RuleFor(usu => usu.Senha)
                .SetValidator(new ValidacaoSenha<RequestCriaUsuarioJson>());
        }
    }
}
