using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDespesaSomenteAtualizacao
{
    Task<Despesa?> BuscaPorId(long id);
    void AtualizaDespesa(Despesa despesa);
}
