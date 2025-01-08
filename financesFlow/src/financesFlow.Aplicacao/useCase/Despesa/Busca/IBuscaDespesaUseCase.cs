using financesFlow.Comunicacao.Responses.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesa.Busca
{
    public interface IBuscaDespesaUseCase
    {
        Task<ResponseDespesasJson> Execute();
    }
}
