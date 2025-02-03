using CommonTestsUtilitis.Entidades;
using CommonTestsUtilitis.Login;
using CommonTestsUtilitis.Mapper;
using DocumentFormat.OpenXml.Spreadsheet;
using financesFlow.Exception.ExceptionsBase;
using financesFlow.Exception;
using FluentAssertions;
using PdfSharp.Drawing;
using CommonTestsUtilitis.Requests;
using CommonTestUtilities.Entities;
using financesFlow.Aplicacao.useCase.Despesas.Atualiza;
using financesFlow.Dominio.Entidades;
using CommonTestsUtilitis.Repositories;

namespace UseCases.Test.Despesas.Atualizar
{
    public class AtualizarDespesaUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var request = RequestDespesaJsonBuilder.Build();
            var expense = DespesaBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id, request);

            await act.Should().NotThrowAsync();

            expense.NomeDespesa.Should().Be(request.NomeDespesa);
            expense.Descricao.Should().Be(request.Descricao);
            expense.DataDespesa.Should().Be(request.DataDespesa);
            expense.ValorDespesa.Should().Be(request.ValorDespesa);
            expense.MetodoPagamento.Should().Be((financesFlow.Dominio.Enums.MetodoPagamento)request.MetodoPagamento);
        }

        [Fact]
        public async Task Error_Title_Empty()
        {
            var loggedUser = UserBuilder.Build();
            var expense = DespesaBuilder.Build(loggedUser);

            var request = RequestDespesaJsonBuilder.Build();
            request.NomeDespesa = string.Empty;

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id, request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.NOME_VAZIO));
        }

        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();

            var request = RequestDespesaJsonBuilder.Build();

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id: 1000, request);
            var result = await act.Should().ThrowAsync<NotFoundException>();

            result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA));
        }

        private AtualizaDespesaUseCase CreateUseCase(Usuario user, Despesa? expense = null)
        {
            var repository = new RepositorioDespesaSomenteAtualizacaoBuilder().BuscaPorId(user, expense).Build();
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnidadeDeTrabalhoBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new AtualizaDespesaUseCase(repository, unitOfWork, mapper, loggedUser);
        }
    }
}
