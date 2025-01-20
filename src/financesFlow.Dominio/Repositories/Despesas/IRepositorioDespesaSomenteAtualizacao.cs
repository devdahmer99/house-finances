using financesFlow.Dominio.Entidades;

namespace financesFlow.Dominio.Repositories.Despesas;
public interface IRepositorioDespesaSomenteAtualizacao
{
    Task<Despesa?> BuscaPorId(Usuario usuario,long id);
    void AtualizaDespesa(Despesa despesa);
}
