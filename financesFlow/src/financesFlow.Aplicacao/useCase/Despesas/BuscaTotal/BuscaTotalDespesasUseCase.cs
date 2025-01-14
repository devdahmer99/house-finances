using financesFlow.Dominio.Repositories.Despesas;

namespace financesFlow.Aplicacao.useCase.Despesas.BuscaTotal;
public class BuscaTotalDespesasUseCase : IBuscaTotalDespesasUseCase
{
    private readonly IRepositorioDespesaSomenteLeitura _repositorio;
    public BuscaTotalDespesasUseCase(IRepositorioDespesaSomenteLeitura repositorio)
    {
        _repositorio = repositorio;        
    }

    public async Task<decimal> Execute()
    {
        var resultado = await _repositorio.BuscaTotalDespesas();
        return resultado;
    }
}
