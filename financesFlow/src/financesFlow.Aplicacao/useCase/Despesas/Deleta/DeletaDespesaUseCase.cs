using financesFlow.Aplicacao.useCase.Despesas.Deleta;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesas.Deleta;
public class DeletaDespesaUseCase : IDeletaDespesaUseCase
{
    private readonly IRepositorioDepesaSomenteEscrita _repositorioDepesaSomenteEscrita;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    public DeletaDespesaUseCase(IRepositorioDepesaSomenteEscrita repositorioDepesaSomenteEscrita, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorioDepesaSomenteEscrita = repositorioDepesaSomenteEscrita;
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
