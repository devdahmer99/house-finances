using financesFlow.Comunicacao.Enums;
using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;

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
        var NomeVazio = string.IsNullOrWhiteSpace(requestDespesa.NomeDespesa);
        var DataValidacao = DateTime.Compare(requestDespesa.DataDespesa, DateTime.UtcNow);
        var MetodoPagamentoValido = Enum.IsDefined(typeof(MetodoPagamento), requestDespesa.FormaDePagamento);

        if(NomeVazio)
        {
            throw new ArgumentException("O nome não pode ser vazio!");
        }

        if(requestDespesa.ValorDespesa <= 0)
        {
            throw new ArgumentException("O valor deve ser maior que zero!");
        }

        if(DataValidacao > 0)
        {
            throw new ArgumentException("A data não pode ultrapassar o limite do dia atual!");
        }

        if(MetodoPagamentoValido == false)
        {
            throw new ArgumentException("Metodo de pagamento não encontrado!");
        }

    }
}
