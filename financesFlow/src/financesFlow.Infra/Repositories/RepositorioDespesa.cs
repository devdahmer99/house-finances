using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Infra.DataAccess;

namespace financesFlow.Infra.Repositories;
internal class RepositorioDespesa : IRepositorioDespensa
{
    public void AdicionarDespesa(Despesa despesa)
    {
        var dbContext = new financesFlowDbContext();
        dbContext.Despesas.Add(despesa);
        dbContext.SaveChanges();
    }
}
