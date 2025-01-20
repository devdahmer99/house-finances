using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDepesaSomenteEscrita
{
    Task AdicionarDespesa(Despesa despesa);

    Task<bool> DeletaDespesa(long id);
}
