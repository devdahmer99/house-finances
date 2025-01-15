using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesas.Deleta;
public class DeletaDespesaUseCase : IDeletaDespesaUseCase
{
    private readonly IRepositorioDepesaSomenteEscrita _repositorioDepesaSomenteEscrita;
    private readonly IRepositorioDespesaSomenteLeitura _repositorio;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly ILoggedUser _loggedUser;
    public DeletaDespesaUseCase(IRepositorioDepesaSomenteEscrita repositorioDepesaSomenteEscrita, IRepositorioDespesaSomenteLeitura repositorio,
        IUnidadeDeTrabalho unidadeDeTrabalho, ILoggedUser loggedUser)
    {
        _repositorioDepesaSomenteEscrita = repositorioDepesaSomenteEscrita;
        _repositorio = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;       
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _repositorioDepesaSomenteEscrita.DeletaDespesa(loggedUser, id);
        if( result == false)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        await _unidadeDeTrabalho.Commit();
    }
}
