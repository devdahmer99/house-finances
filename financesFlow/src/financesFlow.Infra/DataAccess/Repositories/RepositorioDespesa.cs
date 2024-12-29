using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.DataAccess.Repositories;
internal class RepositorioDespesa : IRepositorioDespesaSomenteLeitura, IRepositorioDepesaSomenteEscrita, IRepositorioDespesaSomenteAtualizacao
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

    public void AtualizaDespesa(Despesa despesa)
    {
        _db.Despesas.Update(despesa);
    }

    async Task<Despesa?> IRepositorioDespesaSomenteLeitura.BuscarPorId(long id)
    {
        return await _db.Despesas.AsNoTracking().FirstOrDefaultAsync(des => des.Id == id);
    }

    async Task<Despesa?> IRepositorioDespesaSomenteAtualizacao.BuscaPorId(long id)
    {
        return await _db.Despesas.FirstOrDefaultAsync(des => des.Id == id);
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

    public void AtualizaDespensa(Despesa despesa)
    {
        _db.Despesas.Update(despesa);
    }

    public async Task<List<Despesa>> FiltraPorMes(DateOnly data)
    {
        var DataInicial = new DateTime(year: data.Year, month: data.Month, day: 1).Date;
        var DiasDoMes = DateTime.DaysInMonth(data.Year, data.Month);
        var DataFinal = new DateTime(year: data.Year, month: data.Month, day: DiasDoMes, hour: 23, minute: 59, second: 59);

        return await _db.Despesas.AsNoTracking().Where(desp => desp.DataDespesa >= DataInicial && desp.DataDespesa <= DataFinal ).OrderBy(desp => desp.DataDespesa).ToListAsync();
    }
}
