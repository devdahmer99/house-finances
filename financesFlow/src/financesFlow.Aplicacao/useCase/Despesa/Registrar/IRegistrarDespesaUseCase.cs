using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Comunicacao.Responses.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesa.Registrar;
public interface IRegistrarDespesaUseCase
{
    Task<ResponseDespesaJson> Execute(RequestDespesaJson requestDespesa);
}
