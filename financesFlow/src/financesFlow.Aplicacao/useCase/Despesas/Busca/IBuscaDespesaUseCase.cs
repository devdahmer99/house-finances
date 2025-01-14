using financesFlow.Comunicacao.Responses.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesas.Busca
{
    public interface IBuscaDespesaUseCase
    {
        Task<ResponseDespesasJson> Execute();
    }
}
