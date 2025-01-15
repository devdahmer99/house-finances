using DocumentFormat.OpenXml.Spreadsheet;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using Moq;
using PdfSharp.Drawing;

namespace CommonTestsUtilitis.Repositories
{
    public class RepositorioDespesaSomenteAtualizacaoBuilder
    {
        private readonly Mock<IRepositorioDespesaSomenteAtualizacao> _repository;

        public RepositorioDespesaSomenteAtualizacaoBuilder()
        {
            _repository = new Mock<IRepositorioDespesaSomenteAtualizacao>();
        }

        public RepositorioDespesaSomenteAtualizacaoBuilder BuscaPorId(Usuario user, Despesa? expense)
        {
            if (expense is not null)
                _repository.Setup(repository => repository.BuscaPorId(user, expense.Id)).ReturnsAsync(expense);

            return this;
        }

        public IRepositorioDespesaSomenteAtualizacao Build() => _repository.Object;
    }
}
