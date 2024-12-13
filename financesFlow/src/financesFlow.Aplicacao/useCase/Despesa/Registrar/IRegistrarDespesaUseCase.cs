using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;

namespace financesFlow.Aplicacao.useCase.Despesa.Registrar;
public interface IRegistrarDespesaUseCase
{
    ResponseDespesaJson Execute(RequestDespesaJson requestDespesa);
}
