using CommonTestsUtilitis.Entidades;
using CommonTestsUtilitis.Login;
using CommonTestsUtilitis.Repositories;
using CommonTestUtilities.Entities;
using financesFlow.Aplicacao.useCase.Despesas.Deleta;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;
using FluentAssertions;
using Moq;

namespace UseCases.Test.Despesas.DeletaDespesa
{
    public class DeletaDespesaUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = DespesaBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id);

            await act.Should().NotThrowAsync();
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

        private DeletaDespesaUseCase CreateUseCase(Usuario user, Despesa? expense = null)
        {
            var repositoryWriteOnly = RepositorioDespesaSomenteEscritaBuilder.Build();
            var repository = new RepositorioDespesaSomenteLeituraBuilder().BuscarPorId(user, expense).Build();
            var unitOfWork = UnidadeDeTrabalhoBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new DeletaDespesaUseCase(repositoryWriteOnly, repository, unitOfWork, loggedUser);
        }

    }
    }
