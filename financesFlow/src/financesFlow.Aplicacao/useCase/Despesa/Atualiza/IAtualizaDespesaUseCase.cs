using financesFlow.Comunicacao.Requests;

namespace financesFlow.Aplicacao.useCase.Despesa.Atualiza;
public interface IAtualizaDespesaUseCase
{
    Task Execute(long id, RequestDespesaJson request);
}
