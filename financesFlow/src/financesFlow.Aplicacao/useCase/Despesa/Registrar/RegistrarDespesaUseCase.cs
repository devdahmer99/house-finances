using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;
using financesFlow.Dominio.Enums;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Exception.ExceptionsBase;

namespace financesFlow.Aplicacao.useCase.Despesa.Registrar;
public class RegistrarDespesaUseCase : IRegistrarDespesaUseCase
{
    private readonly IRepositorioDespensa _repositorio;
    private readonly IUnidadeDeTrabalho _unidade;
    public RegistrarDespesaUseCase(IRepositorioDespensa repositorio, IUnidadeDeTrabalho unidade)
    {
        _repositorio = repositorio;
        _unidade = unidade;
    }
    public async Task<ResponseDespesaJson> Execute(RequestDespesaJson requestDespesa)
    {
        Validate(requestDespesa);

        var Entidade = new Dominio.Entidades.Despesa
        {
            NomeDespesa = requestDespesa.NomeDespesa,
            Descricao = requestDespesa.DescricaoDespesa,
            DataDespesa = requestDespesa.DataDespesa,
            ValorDespesa = requestDespesa.ValorDespesa,
            MetodoPagamento = (MetodoPagamento)requestDespesa.FormaDePagamento
        };

        await _repositorio.AdicionarDespesa(Entidade);
        await _unidade.Commit();

        return new ResponseDespesaJson();
    }

    private void Validate(RequestDespesaJson requestDespesa)
    {
        var validator = new RegistrarValidacaoDespesa();
        var result = validator.Validate(requestDespesa);
        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
