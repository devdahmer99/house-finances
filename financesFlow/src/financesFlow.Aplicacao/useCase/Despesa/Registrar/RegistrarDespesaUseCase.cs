using AutoMapper;
using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesa.Registrar;
public class RegistrarDespesaUseCase : IRegistrarDespesaUseCase
{
    private readonly IRepositorioDepesaSomenteEscrita _repositorio;
    private readonly IUnidadeDeTrabalho _unidade;
    private readonly IMapper _mapper;
    public RegistrarDespesaUseCase(IRepositorioDepesaSomenteEscrita repositorio, IUnidadeDeTrabalho unidade, IMapper mapper)
    {
        _repositorio = repositorio;
        _unidade = unidade;
        _mapper = mapper;
    }
    public async Task<ResponseDespesaJson> Execute(RequestDespesaJson requestDespesa)
    {
        Validate(requestDespesa);

        var Entidade = _mapper.Map<Dominio.Entidades.Despesa>(requestDespesa);

        await _repositorio.AdicionarDespesa(Entidade);
        await _unidade.Commit();

        return _mapper.Map<ResponseDespesaJson>(Entidade);
    }

    private void Validate(RequestDespesaJson requestDespesa)
    {
        var validator = new ValidacaoDespesa();
        var result = validator.Validate(requestDespesa);
        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
