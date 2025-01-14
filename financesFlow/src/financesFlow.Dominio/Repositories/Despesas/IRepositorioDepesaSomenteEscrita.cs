using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDepesaSomenteEscrita
{
    Task AdicionarDespesa(Despesa despesa);
    /// <summary>
    /// Essa função, vai retornar true caso o processo de exclusão da despesa for feito com sucesso
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeletaDespesa(Usuario user, long id);
}
