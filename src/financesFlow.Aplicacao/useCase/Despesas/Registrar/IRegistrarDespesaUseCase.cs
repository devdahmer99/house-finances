using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Comunicacao.Responses.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesas.Registrar;
public interface IRegistrarDespesaUseCase
{
    Task<ResponseDespesaJson> Execute(RequestDespesaJson requestDespesa);
}
