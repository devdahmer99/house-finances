using System.ComponentModel.DataAnnotations;
using CommonTestsUtilitis.Entidades;
using CommonTestsUtilitis.Login;
using CommonTestsUtilitis.Mapper;
using CommonTestsUtilitis.Repositories;
using CommonTestsUtilitis.Requests;
using financesFlow.Aplicacao.useCase.Despesas.Registrar;
using financesFlow.Dominio.Entidades;
using financesFlow.Exception;
using FluentAssertions;

namespace UseCases.Test.Despesas
{
    public class RegisterDespesaUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var request = RequestDespesaJsonBuilder.Build();
            var useCase = CreateUseCase(loggedUser);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.NomeDespesa.Should().Be(request.NomeDespesa);
        }

        [Fact]
        public async Task Error_Title_Empty()
        {
           var loggedUser = UserBuilder.Build();
           var request = RequestDespesaJsonBuilder.Build();
           request.NomeDespesa = string.Empty;

           var useCase = CreateUseCase(loggedUser);

           var act = async () => await useCase.Execute(request);

           var result = await act.Should().ThrowAsync<ValidationException>();

            result.Where(ex => ex.Message.Count() == 1 && ex.Message.Contains(ResourceErrorMessages.NOME_VAZIO));
        }

        private RegistrarDespesaUseCase CreateUseCase(Usuario user)
        {
            var repository = RepositorioDespesaSomenteEscritaBuilder.Build();
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnidadeDeTrabalhoBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new RegistrarDespesaUseCase(repository, unitOfWork, mapper, loggedUser);
        }
    }
}
