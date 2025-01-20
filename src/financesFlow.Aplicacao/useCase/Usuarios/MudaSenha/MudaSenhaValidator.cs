using financesFlow.Comunicacao.Requests.Usuario;
using FluentValidation;

namespace financesFlow.Aplicacao.useCase.Usuarios.MudaSenha
{
    public class MudaSenhaValidator : AbstractValidator<RequestMudaSenhaJson>
    {
        public MudaSenhaValidator()
        {
            RuleFor(x => x.NovaSenha).SetValidator(new ValidacaoSenha<RequestMudaSenhaJson>());
        }
    }
}
