using System.Runtime.CompilerServices;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Despesa>> BuscarTudo()
    {
        return await _db.Despesas.ToListAsync();
    }
}
