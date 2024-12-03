using financesFlow.Comunicacao.Requests;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Security.Cryptography;

namespace financesFlow.Aplicacao.useCase.Despesa.Registrar;
public class RegistrarValidacaoDespesa : AbstractValidator<RequestDespesaJson>
{
    public RegistrarValidacaoDespesa()
    {
        RuleFor(despesa => despesa.NomeDespesa).NotEmpty().WithMessage("O nome não pode ser vazio!");
        RuleFor(despesa => despesa.ValorDespesa).GreaterThan(0).WithMessage("O valor não pode ser igual a zero");
        RuleFor(despesa => despesa.DataDespesa).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data não pode ultrapassar o limite do dia atual!");
        RuleFor(despesa => despesa.FormaDePagamento).IsInEnum().WithMessage("Metodo de pagamento não encontrado!");
    }
}
