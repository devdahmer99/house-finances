using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDespesaSomenteLeitura
{
    Task<List<Despesa>> BuscarTudo();
    Task<Despesa?> BuscarPorId(long id);
    Task<List<Despesa>> FiltraPorMes(DateOnly data);
    Task<decimal> BuscaTotalDespesas();
}
