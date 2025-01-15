using AutoMapper;
using financesFlow.Aplicacao.useCase.Despesas.BuscaPorId;
using financesFlow.Comunicacao.Responses.Despesa;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesas.BuscaPorId;
public class BuscaDespesaPorIdUseCase : IBuscaDespesaPorIdUseCase
{
    private readonly IRepositorioDespesaSomenteLeitura _repositorio;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public BuscaDespesaPorIdUseCase(IRepositorioDespesaSomenteLeitura repositorio, IMapper mapper, ILoggedUser loggedUser)
    {
        _repositorio = repositorio;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseDespesaJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var resultado = await _repositorio.BuscarPorId(loggedUser, id);
        if(resultado == null)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        return _mapper.Map<ResponseDespesaJson>(resultado);
    }
}
