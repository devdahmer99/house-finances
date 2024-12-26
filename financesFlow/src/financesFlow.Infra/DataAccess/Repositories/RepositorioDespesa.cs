using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.DataAccess.Repositories;
internal class RepositorioDespesa : IRepositorioDespesaSomenteLeitura, IRepositorioDepesaSomenteEscrita
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

    public async Task<Despesa?> BuscarPorId(long id)
    {
        return await _db.Despesas.AsNoTracking().FirstOrDefaultAsync(des => des.Id == id);
    }

    public async Task<List<Despesa>> BuscarTudo()
    {
        return await _db.Despesas.AsNoTracking().ToListAsync();
    }

    public async Task<bool> DeletaDespesa(long id)
    {
        var result = await _db.Despesas.FirstOrDefaultAsync(des => des.Id == id);
        if(result == null)
        {
            return false;
        }

        _db.Despesas.Remove(result);

        return true;
    }
}
