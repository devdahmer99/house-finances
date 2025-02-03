using AutoMapper;
using financesFlow.Comunicacao.Responses.Despesa;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Dominio.Services.LoggedUser;

namespace financesFlow.Aplicacao.useCase.Despesas.Busca
{
    public class BuscaDespesaUseCase : IBuscaDespesaUseCase
    {
        private readonly IRepositorioDespesaSomenteLeitura _repositorio;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public BuscaDespesaUseCase(IRepositorioDespesaSomenteLeitura repositorio, IMapper mapper, ILoggedUser loggedUser)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseDespesasJson> Execute()
        {
            var loggedUser = await _loggedUser.Get();
            var resultado = await _repositorio.BuscarTudo(loggedUser);

            return new ResponseDespesasJson
            {
                Despesas = _mapper.Map <List<ResponseShortDepesaJson>>(resultado)
            };
        }

    }
}
