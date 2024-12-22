using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDespesa
{
    Task AdicionarDespesa(Despesa despesa);
    Task<List<Despesa>> BuscarTudo();
    Task<Despesa?> BuscarPorId(long id);
}
