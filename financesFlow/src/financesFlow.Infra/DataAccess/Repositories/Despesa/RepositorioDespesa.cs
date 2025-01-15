using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.DataAccess.Repositories.Despesa;
internal class RepositorioDespesa : IRepositorioDespesaSomenteLeitura, IRepositorioDepesaSomenteEscrita, IRepositorioDespesaSomenteAtualizacao
{
    private readonly financesFlowDbContext _db;
    public RepositorioDespesa(financesFlowDbContext dbContext)
    {
        _db = dbContext;
    }
    public async Task AdicionarDespesa(Dominio.Entidades.Despesa despesa)
    {
        await _db.Despesas.AddAsync(despesa);
    }

    public void AtualizaDespesa(Dominio.Entidades.Despesa despesa)
    {
        _db.Despesas.Update(despesa);
    }

    async Task<Dominio.Entidades.Despesa?> IRepositorioDespesaSomenteLeitura.BuscarPorId(Dominio.Entidades.Usuario user, long id)
    {
        return await _db.Despesas.AsNoTracking().FirstOrDefaultAsync(des => des.Id == id && des.UsuarioId == user.Id);
    }

    async Task<Dominio.Entidades.Despesa?> IRepositorioDespesaSomenteAtualizacao.BuscaPorId(Dominio.Entidades.Usuario user, long id)
    {
        return await _db.Despesas.FirstOrDefaultAsync(des => des.Id == id && des.UsuarioId == user.Id);
    }

    public async Task<List<Dominio.Entidades.Despesa>> BuscarTudo()
    {
        return await _db.Despesas.AsNoTracking().OrderBy(des => des.DataDespesa).ThenBy(des => des.Id).Take(10).ToListAsync();
    }

    public async Task<bool> DeletaDespesa(Dominio.Entidades.Usuario user, long id)
    {
        var result = await _db.Despesas.FirstOrDefaultAsync(des => des.Id == id && des.UsuarioId == user.Id);
        if (result == null)
        {
            return false;
        }

        _db.Despesas.Remove(result);

        return true;
    }

    public void AtualizaDespensa(Dominio.Entidades.Despesa despesa)
    {
        _db.Despesas.Update(despesa);
    }

    public async Task<List<Dominio.Entidades.Despesa>> FiltraPorMes(Dominio.Entidades.Usuario user, DateOnly data)
    {
        var DataInicial = new DateTime(year: data.Year, month: data.Month, day: 1).Date;
        var DiasDoMes = DateTime.DaysInMonth(data.Year, data.Month);
        var DataFinal = new DateTime(year: data.Year, month: data.Month, day: DiasDoMes, hour: 23, minute: 59, second: 59);

        return await _db.Despesas.AsNoTracking()
            .Where(desp => desp.UsuarioId == user.Id && desp.DataDespesa >= DataInicial && desp.DataDespesa <= DataFinal).OrderBy(desp => desp.DataDespesa).ToListAsync();
    }

    public async Task<decimal> BuscaTotalDespesas()
    {
        decimal totalDespesas = await _db.Despesas.SumAsync(des => des.ValorDespesa);
        return totalDespesas;
    }

    public Task<List<Dominio.Entidades.Despesa>> BuscarTudo(Dominio.Entidades.Usuario user)
    {
        throw new NotImplementedException();
    }

    public Task<Dominio.Entidades.Despesa?> BuscarPorId(Dominio.Entidades.Usuario user, long id)
    {
        throw new NotImplementedException();
    }
}
