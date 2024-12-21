using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;

namespace financesFlow.Aplicacao.useCase.Despesa.Busca
{
    public interface IBuscaDespesaUseCase
    {
        Task<ResponseDespesasJson> Execute();
    }
}
