using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Usuarios;
using financesFlow.Dominio.Services.LoggedUser;

namespace financesFlow.Aplicacao.useCase.Usuarios.Delete
{
    public class DeleteUserAccountUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IRepositorioUsuarioSomenteEscrita _repository;
        private readonly IUnidadeDeTrabalho _unitOfWork;

        public DeleteUserAccountUseCase(
            IUnidadeDeTrabalho unitOfWork,
            IRepositorioUsuarioSomenteEscrita repository,
            ILoggedUser loggedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
        }

        public async Task Execute()
        {
            var user = await _loggedUser.Get();

            await _repository.Delete(user);

            await _unitOfWork.Commit();
        }
    }
}
