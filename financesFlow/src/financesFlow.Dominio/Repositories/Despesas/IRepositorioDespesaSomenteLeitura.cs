using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDespesaSomenteLeitura
{
    Task<List<Despesa>> BuscarTudo(Usuario user);
    Task<Despesa?> BuscarPorId(Usuario user, long id);
    Task<List<Despesa>> FiltraPorMes(Dominio.Entidades.Usuario user, DateOnly data);
    Task<decimal> BuscaTotalDespesas();
}
