using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Exception;
using FluentValidation;

namespace financesFlow.Aplicacao.useCase.Usuarios.Criar
{
    public class ValidacaoRegistrarUsuario : AbstractValidator<RequestCriaUsuarioJson>
    {
        public ValidacaoRegistrarUsuario()
        {
            RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_VAZIO);
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EMAIL_VAZIO)
                .EmailAddress()
                .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
                .WithMessage(ResourceErrorMessages.EMAIL_INVALIDO);

            RuleFor(user => user.Senha).SetValidator(new ValidacaoSenha<RequestCriaUsuarioJson>());
        }
    }
}
