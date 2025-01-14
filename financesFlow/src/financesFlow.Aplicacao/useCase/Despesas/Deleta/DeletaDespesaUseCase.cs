using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesas.Deleta;
public class DeletaDespesaUseCase : IDeletaDespesaUseCase
{
    private readonly IRepositorioDepesaSomenteEscrita _repositorioDepesaSomenteEscrita;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly ILoggedUser _loggedUser;
    public DeletaDespesaUseCase(IRepositorioDepesaSomenteEscrita repositorioDepesaSomenteEscrita,
        IUnidadeDeTrabalho unidadeDeTrabalho, ILoggedUser loggedUser)
    {
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorioDepesaSomenteEscrita = repositorioDepesaSomenteEscrita;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var result = await _repositorioDepesaSomenteEscrita.DeletaDespesa(id);
        if( result == false)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        await _unidadeDeTrabalho.Commit();
    }
}
