using AutoMapper;
using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;
using financesFlow.Dominio.Repositories.Despesas;

namespace financesFlow.Aplicacao.useCase.Despesa.Busca
{
    public class BuscaDespesaUseCase : IBuscaDespesaUseCase
    {
        private readonly IRepositorioDespensa _repositorio;
        private readonly IMapper _mapper;

        public BuscaDespesaUseCase(IRepositorioDespensa repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        public async Task<ResponseDespesasJson> Execute()
        {
            var resultado = await _repositorio.BuscarTudo();

            return new ResponseDespesasJson
            {
                Despesas = _mapper.Map <List<ResponseShortDepesaJson>>(resultado)
            };
        }

    }
}
