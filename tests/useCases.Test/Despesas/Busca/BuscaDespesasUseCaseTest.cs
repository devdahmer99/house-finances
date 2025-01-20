using CommonTestsUtilitis.Entidades;
using CommonTestsUtilitis.Login;
using CommonTestsUtilitis.Mapper;
using CommonTestsUtilitis.Repositories;
using CommonTestUtilities.Entities;
using financesFlow.Aplicacao.useCase.Despesas.Busca;
using financesFlow.Dominio.Entidades;
using FluentAssertions;

namespace UseCases.Test.Despesas.Busca
{
    public class BuscaDespesasUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expenses = DespesaBuilder.Collection(loggedUser);

            var useCase = CreateUseCase(loggedUser, expenses);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Despesas.Should().NotBeNullOrEmpty().And.AllSatisfy(expense =>
            {
                expense.Id.Should().BeGreaterThan(0);
                expense.NomeDespesa.Should().NotBeNullOrEmpty();
                expense.ValorDespesa.Should().BeGreaterThan(0);
            });
        }

        private BuscaDespesaUseCase CreateUseCase(Usuario user, List<Despesa> expenses)
        {
            var repository = new RepositorioDespesaSomenteLeituraBuilder().BuscarTudo(user, expenses).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new BuscaDespesaUseCase(repository, mapper, loggedUser);
        }
    }
}
