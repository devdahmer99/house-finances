using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception.ExceptionsBase;
using financesFlow.Exception;
using FluentValidation.Results;
using financesFlow.Dominio.Repositories.Usuarios;
using financesFlow.Dominio.Repositories;
using financesFlow.Comunicacao.Requests.Usuario;

namespace financesFlow.Aplicacao.useCase.Usuarios.Atualizar
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IRepositorioUsuarioSomenteAtualizacao _repository;
        private readonly IRepositorioUsuarioSomenteLeitura _userReadOnlyRepository;
        private readonly IUnidadeDeTrabalho _unitOfWork;

        public UpdateUserUseCase(
            ILoggedUser loggedUser,
            IRepositorioUsuarioSomenteAtualizacao repository,
            IRepositorioUsuarioSomenteLeitura userReadOnlyRepository,
            IUnidadeDeTrabalho unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task Execute(RequestAtualizaUsuarioJson request)
        {
            var loggedUser = await _loggedUser.Get();

            await Validate(request, loggedUser.Email);

            var user = await _repository.GetById(loggedUser.Id);

            user.Nome = request.Nome;
            user.Email = request.Email;

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestAtualizaUsuarioJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = validator.Validate(request);

            if (currentEmail.Equals(request.Email) == false)
            {
                var userExist = await _userReadOnlyRepository.ExisteUsuarioAtivoComEmail(request.Email);
                if (userExist)
                    result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_EXISTE));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
