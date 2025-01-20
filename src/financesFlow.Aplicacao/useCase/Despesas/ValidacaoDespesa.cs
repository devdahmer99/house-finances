using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Exception;
using FluentValidation;

namespace financesFlow.Aplicacao.useCase.Despesas;
public class ValidacaoDespesa : AbstractValidator<RequestDespesaJson>
{
    public ValidacaoDespesa()
    {
        RuleFor(despesa => despesa.NomeDespesa).NotEmpty().WithMessage(ResourceErrorMessages.NOME_VAZIO);
        RuleFor(despesa => despesa.ValorDespesa).GreaterThan(0).WithMessage(ResourceErrorMessages.VALOR_DESPESA);
        RuleFor(despesa => despesa.DataDespesa).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DATA_INCORRETA_OU_MAIOR);
        RuleFor(despesa => despesa.MetodoPagamento).IsInEnum().WithMessage(ResourceErrorMessages.METODO_PAGAMENTO_INEXISTENTE);
    }
}
