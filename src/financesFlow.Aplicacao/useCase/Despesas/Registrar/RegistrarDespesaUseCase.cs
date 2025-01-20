using AutoMapper;
using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Comunicacao.Responses.Despesa;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesas.Registrar;
public class RegistrarDespesaUseCase : IRegistrarDespesaUseCase
{
    private readonly IRepositorioDepesaSomenteEscrita _repositorio;
    private readonly IUnidadeDeTrabalho _unidade;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegistrarDespesaUseCase(IRepositorioDepesaSomenteEscrita repositorio, 
        IUnidadeDeTrabalho unidade, IMapper mapper, ILoggedUser loggedUser)
    {
        _repositorio = repositorio;
        _unidade = unidade;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseDespesaJson> Execute(RequestDespesaJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var despesa = _mapper.Map<Despesa>(request);

        despesa.UsuarioId = loggedUser.Id;

        await _repositorio.AdicionarDespesa(despesa);

        await _unidade.Commit();

        return _mapper.Map<ResponseDespesaJson>(despesa);
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
