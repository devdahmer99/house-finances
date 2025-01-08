using financesFlow.Comunicacao.Responses.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesa.BuscaPorId;
public interface IBuscaDespesaPorIdUseCase
{
    Task<ResponseDespesaJson> Execute(long id);
}
