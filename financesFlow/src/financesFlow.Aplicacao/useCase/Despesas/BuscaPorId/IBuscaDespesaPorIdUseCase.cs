using financesFlow.Comunicacao.Responses.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesas.BuscaPorId;
public interface IBuscaDespesaPorIdUseCase
{
    Task<ResponseDespesaJson> Execute(long id);
}
