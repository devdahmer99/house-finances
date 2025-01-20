using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesas.Deleta;
public class DeletaDespesaUseCase : IDeletaDespesaUseCase
{
    private readonly IRepositorioDepesaSomenteEscrita _repositorio;
    private readonly IRepositorioDespesaSomenteLeitura _repositorioLeitura;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly ILoggedUser _loggedUser;
    public DeletaDespesaUseCase(IRepositorioDepesaSomenteEscrita repositorioDepesaSomenteEscrita, IRepositorioDespesaSomenteLeitura repositorio,
        IUnidadeDeTrabalho unidadeDeTrabalho, ILoggedUser loggedUser)
    {
        _repositorio = repositorioDepesaSomenteEscrita;
        _repositorioLeitura = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;       
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var expense = await _repositorioLeitura.BuscarPorId(loggedUser, id);
        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        await _repositorio.DeletaDespesa(id);
        await _unidadeDeTrabalho.Commit();
    }
}
