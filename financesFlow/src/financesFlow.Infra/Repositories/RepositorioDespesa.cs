using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Infra.DataAccess;

namespace financesFlow.Infra.Repositories;
internal class RepositorioDespesa : IRepositorioDespensa
{
    private readonly financesFlowDbContext _db;
    public RepositorioDespesa(financesFlowDbContext dbContext)
    {
        _db = dbContext;
    }
    public void AdicionarDespesa(Despesa despesa)
    {

        _db.Despesas.Add(despesa);
        _db.SaveChanges();
    }
}
