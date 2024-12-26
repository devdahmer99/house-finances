
using AutoMapper;
using financesFlow.Comunicacao.Requests;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesa.Atualiza;
public class AtualizaDespesaUseCase : IAtualizaDespesaUseCase
{
    private readonly IRepositorioDespesaSomenteAtualizacao _repositorioSomenteAtualizacao;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IMapper _mapper;
    public AtualizaDespesaUseCase(IRepositorioDespesaSomenteAtualizacao repositorioSomenteAtualizacao, IUnidadeDeTrabalho unidadeDeTrabalho, IMapper mapper)
    {
        _repositorioSomenteAtualizacao = repositorioSomenteAtualizacao;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _mapper = mapper;
    }
    public async Task Execute(long id, RequestDespesaJson request)
    {
        Validate(request);

        var despesa = await _repositorioSomenteAtualizacao.BuscaPorId(id);

        if(despesa is null)
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
