using AutoMapper;
using financesFlow.Comunicacao.Responses;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesa.BuscaPorId;
public class BuscaDespesaPorIdUseCase : IBuscaDespesaPorIdUseCase
{
    private readonly IRepositorioDespesaSomenteLeitura _repositorio;
    private readonly IMapper _mapper;
    public BuscaDespesaPorIdUseCase(IRepositorioDespesaSomenteLeitura repositorio, IMapper mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }
    public async Task<ResponseDespesaJson> Execute(long id)
    {
        var resultado = await _repositorio.BuscarPorId(id);
        if(resultado == null)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        return _mapper.Map<ResponseDespesaJson>(resultado);
    }
}
