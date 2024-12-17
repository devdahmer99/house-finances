using System.Runtime.CompilerServices;
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
    public async Task AdicionarDespesa(Despesa despesa)
    { 
        await _db.Despesas.AddAsync(despesa);
    }
}
