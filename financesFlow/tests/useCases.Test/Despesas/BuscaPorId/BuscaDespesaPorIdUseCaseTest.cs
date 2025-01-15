using CommonTestsUtilitis.Entidades;
using CommonTestsUtilitis.Login;
using CommonTestsUtilitis.Mapper;
using CommonTestsUtilitis.Repositories;
using CommonTestUtilities.Entities;
using financesFlow.Aplicacao.useCase.Despesas.BuscaPorId;
using financesFlow.Dominio.Entidades;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.Despesas.BuscaPorId
{
    public class GetExpenseByIdUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = DespesaBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var result = await useCase.Execute(expense.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(expense.Id);
            result.NomeDespesa.Should().Be(expense.NomeDespesa);
            result.Descricao.Should().Be(expense.Descricao);
            result.DataDespesa.Should().Be(expense.DataDespesa);
            result.ValorDespesa.Should().Be(expense.ValorDespesa);
            result.MetodoPagamento.Should().Be((financesFlow.Comunicacao.Enums.MetodoPagamento)expense.MetodoPagamento);
            result.Tags.Should().NotBeNullOrEmpty().And.BeEquivalentTo(expense.Tags.Select(tag => tag.Value));
        }

        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id: 1000);
            var result = await act.Should().ThrowAsync<NotFoundException>();

            result.Where(ex => ex.BuscaErrors().Count == 1 && ex.BuscaErrors().Contains(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA));
        }

        private BuscaDespesaPorIdUseCase CreateUseCase(Usuario user, Despesa? expense = null)
        {
            var repository = new RepositorioDespesaSomenteLeituraBuilder().BuscarPorId(user, expense).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new BuscaDespesaPorIdUseCase(repository, mapper, loggedUser);
        }
    }
