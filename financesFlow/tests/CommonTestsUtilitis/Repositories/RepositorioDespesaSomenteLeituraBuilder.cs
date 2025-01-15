using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using Moq;

namespace CommonTestsUtilitis.Repositories
{
    public class RepositorioDespesaSomenteLeituraBuilder
    {
        private readonly Mock<IRepositorioDespesaSomenteLeitura> _repository;

        public RepositorioDespesaSomenteLeituraBuilder()
        {
            _repository = new Mock<IRepositorioDespesaSomenteLeitura>();
        }

        public RepositorioDespesaSomenteLeituraBuilder BuscarTudo(Usuario user, List<Despesa> expenses)
        {
            _repository.Setup(repository => repository.BuscarTudo(user)).ReturnsAsync(expenses);
            return this;
        }

        public RepositorioDespesaSomenteLeituraBuilder BuscarPorId(Usuario user, Despesa? expense)
        {
            if (expense is not null)
                _repository.Setup(repository => repository.BuscarPorId(user, expense.Id)).ReturnsAsync(expense);
            return this;
        }

        public RepositorioDespesaSomenteLeituraBuilder FiltraPorMes(Usuario user, List<Despesa> expenses)
        {
            _repository.Setup(repository => repository.FiltraPorMes(user, It.IsAny<DateOnly>())).ReturnsAsync(expenses);

            return this;
        }

        public IRepositorioDespesaSomenteLeitura Build() => _repository.Object;
    }
}
