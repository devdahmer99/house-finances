using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Exception;
using FluentValidation;

namespace financesFlow.Aplicacao.useCase.Usuarios.Criar
{
    public class ValidacaoRegistrarUsuario : AbstractValidator<RequestCriaUsuarioJson>
    {
        public ValidacaoRegistrarUsuario()
        {
            RuleFor(user => user.Nome).NotEmpty()
                .WithMessage(ResourceErrorMessages.USUARIO_COM_NOME_VAZIO);
            RuleFor(user => user.Email).NotEmpty()
                .WithMessage(ResourceErrorMessages.USUARIO_COM_EMAIL_VAZIO)
                .EmailAddress().
                WithMessage(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS);
        }
    }
}
