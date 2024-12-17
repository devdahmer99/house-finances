using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;

namespace financesFlow.Aplicacao.useCase.Despesa.Registrar;
public interface IRegistrarDespesaUseCase
{
    Task<ResponseDespesaJson> Execute(RequestDespesaJson requestDespesa);
}
