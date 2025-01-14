using financesFlow.Comunicacao.Requests.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesas.Atualiza;
public interface IAtualizaDespesaUseCase
{
    Task Execute(long id, RequestDespesaJson request);
}
