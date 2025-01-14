
using AutoMapper;
using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesas.Atualiza;
public class AtualizaDespesaUseCase : IAtualizaDespesaUseCase
{
    private readonly IRepositorioDespesaSomenteAtualizacao _repositorioSomenteAtualizacao;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public AtualizaDespesaUseCase(IRepositorioDespesaSomenteAtualizacao repositorioSomenteAtualizacao,
        IUnidadeDeTrabalho unidadeDeTrabalho, IMapper mapper, ILoggedUser loggedUser)
    {
        _repositorioSomenteAtualizacao = repositorioSomenteAtualizacao;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task Execute(long id, RequestDespesaJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var despesa = await _repositorioSomenteAtualizacao.BuscaPorId(loggedUser, id);

        if (despesa is null)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        _mapper.Map(request,despesa);
        _repositorioSomenteAtualizacao.AtualizaDespesa(despesa);

        await _unidadeDeTrabalho.Commit();
    }

    private void Validate(RequestDespesaJson request)
    {
        var validator = new ValidacaoDespesa();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
