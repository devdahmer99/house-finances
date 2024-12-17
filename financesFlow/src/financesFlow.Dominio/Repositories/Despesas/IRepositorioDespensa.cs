using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDespensa
{
    Task AdicionarDespesa(Despesa despesa);
}
