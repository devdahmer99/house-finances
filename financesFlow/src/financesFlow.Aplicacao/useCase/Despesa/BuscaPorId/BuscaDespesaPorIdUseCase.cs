using AutoMapper;
using financesFlow.Comunicacao.Responses;
using financesFlow.Dominio.Repositories.Despesas;

namespace financesFlow.Aplicacao.useCase.Despesa.BuscaPorId;
public class BuscaDespesaPorIdUseCase : IBuscaDespesaPorIdUseCase
{
    private readonly IRepositorioDespesa _repositorio;
    private readonly IMapper _mapper;
    public BuscaDespesaPorIdUseCase(IRepositorioDespesa repositorio, IMapper mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }
    public async Task<ResponseDespesaJson> Execute(long id)
    {
        var resultado = await _repositorio.BuscarPorId(id);

        return _mapper.Map<ResponseDespesaJson>(resultado);
    }
}
