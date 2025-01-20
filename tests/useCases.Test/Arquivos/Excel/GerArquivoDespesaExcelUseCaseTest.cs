using CommonTestsUtilitis.Entidades;
using CommonTestsUtilitis.Login;
using CommonTestsUtilitis.Repositories;
using CommonTestUtilities.Entities;
using financesFlow.Aplicacao.useCase.Arquivo.Excel;
using financesFlow.Dominio.Entidades;
using FluentAssertions;

namespace UseCases.Test.Arquivos.Excel
{
    public class GerArquivoDespesaExcelUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expenses = DespesaBuilder.Collection(loggedUser);

            var useCase = CreateUseCase(loggedUser, expenses);

            var result = await useCase.GeraArquivo(DateOnly.FromDateTime(DateTime.Today));

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Success_Empty()
        {
            var loggedUser = UserBuilder.Build();

            var useCase = CreateUseCase(loggedUser, new List<Despesa>());
            var result = await useCase.GeraArquivo(DateOnly.FromDateTime(DateTime.Today));
            result.Should().BeEmpty();
        }

        private GeraArquivoExcelDespesaUseCase CreateUseCase(Usuario user, List<Despesa> expenses)
        {
            var repository = new RepositorioDespesaSomenteLeituraBuilder().FiltraPorMes(user, expenses).Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GeraArquivoExcelDespesaUseCase(repository, loggedUser);
        }
    }
}
