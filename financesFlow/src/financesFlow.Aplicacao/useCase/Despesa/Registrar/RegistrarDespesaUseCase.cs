using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesa.Registrar;
public class RegistrarDespesaUseCase
{
    public ResponseDespesaJson Execute(RequestDespesaJson requestDespesa)
    {
        Validate(requestDespesa);

        return new ResponseDespesaJson
        {
            Despesa = requestDespesa.NomeDespesa,
            Descricao = requestDespesa.DescricaoDespesa,
            Data = requestDespesa.DataDespesa,
            Valor = requestDespesa.ValorDespesa,
            Pagamento = requestDespesa.FormaDePagamento,
        };
    }

    private void Validate(RequestDespesaJson requestDespesa)
    {
        var validator = new RegistrarValidacaoDespesa();
        var result = validator.Validate(requestDespesa);
        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
