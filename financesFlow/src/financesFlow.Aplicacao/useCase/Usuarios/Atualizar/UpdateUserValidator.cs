using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Exception;
using FluentValidation;

namespace financesFlow.Aplicacao.useCase.Usuarios.Atualizar
{
    public class UpdateUserValidator : AbstractValidator<RequestAtualizaUsuarioJson>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_VAZIO);
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EMAIL_VAZIO)
                .EmailAddress()
                .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
                .WithMessage(ResourceErrorMessages.EMAIL_INVALIDO);
        }
    }
}
