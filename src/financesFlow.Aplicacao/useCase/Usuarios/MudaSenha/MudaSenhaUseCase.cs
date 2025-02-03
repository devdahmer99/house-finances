using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Usuarios;
using financesFlow.Dominio.Seguranca.Criptografia;
using financesFlow.Dominio.Services.LoggedUser;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace financesFlow.Aplicacao.useCase.Usuarios.MudaSenha
{
    public class MudaSenhaUseCase
    {
        public class ChangePasswordUseCase : IMudaSenhaUseCase
        {
            private readonly ILoggedUser _loggedUser;
            private readonly IRepositorioUsuarioSomenteAtualizacao _repository;
            private readonly IUnidadeDeTrabalho _unitOfWork;
            private readonly IEncriptadorSenha _passwordEncripter;

            public ChangePasswordUseCase(
                ILoggedUser loggedUser,
                IEncriptadorSenha passwordEncripter,
                IRepositorioUsuarioSomenteAtualizacao repository,
                IUnidadeDeTrabalho unitOfWork)
            {
                _loggedUser = loggedUser;
                _repository = repository;
                _unitOfWork = unitOfWork;
                _passwordEncripter = passwordEncripter;
            }

            public async Task Execute(RequestMudaSenhaJson request)
            {
                var loggedUser = await _loggedUser.Get();

                Validate(request, loggedUser);

                var user = await _repository.GetById(loggedUser.Id);
                user.Senha = _passwordEncripter.Encript(request.NovaSenha);

                _repository.Update(user);

                await _unitOfWork.Commit();
            }

            private void Validate(RequestMudaSenhaJson request, Dominio.Entidades.Usuario loggedUser)
            {
                var validator = new MudaSenhaValidator();

                var result = validator.Validate(request);

                var passwordMatch = _passwordEncripter.Verify(request.Senha, loggedUser.Senha);

                if (passwordMatch == false)
                {
                    result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.SENHA_DIGITADA_NAO_BATEM));
                }

                if (result.IsValid == false)
                {
                    var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                    throw new ErrorOnValidationException(errors);
                }
            }
        }
    }
}
