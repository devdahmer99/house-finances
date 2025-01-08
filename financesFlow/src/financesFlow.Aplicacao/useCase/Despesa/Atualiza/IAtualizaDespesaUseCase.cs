using financesFlow.Comunicacao.Requests.Despesa;

namespace financesFlow.Aplicacao.useCase.Despesa.Atualiza;
public interface IAtualizaDespesaUseCase
{
    Task Execute(long id, RequestDespesaJson request);
}
