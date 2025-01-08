using AutoMapper;
using financesFlow.Dominio.Repositories.Despesas;
using financesFlow.Comunicacao.Responses.Usuario;
using financesFlow.Dominio.Repositories;
using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories.Usuarios;
using financesFlow.Aplicacao.useCase.Despesa;
using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Exception.ExceptionsBase;
using FluentValidation;

namespace financesFlow.Aplicacao.useCase.Usuarios.Criar
{
    public class CriaUsuarioUseCase : ICriarUsuarioUseCase
    {
        private readonly IRepositorioUsuarioSomenteEscrita _repositorio;
        private readonly IUnidadeDeTrabalho _unidade;
        private readonly IMapper _mapper;

        public CriaUsuarioUseCase(IRepositorioUsuarioSomenteEscrita repositorio, IUnidadeDeTrabalho unidade, IMapper mapper)
        {
            _repositorio = repositorio;
            _unidade = unidade;
            _mapper = mapper;
        }

        public async Task<ResponseCriaUsuarioJson> Execute(RequestCriaUsuarioJson request)
        {
            var Entidade = _mapper.Map<Usuario>(request);
            await _repositorio.CriaUsuario(Entidade);
            await _unidade.Commit();

            return _mapper.Map<ResponseCriaUsuarioJson>(Entidade);
        }

        private void Validate(RequestCriaUsuarioJson requestUsuario)
        {
            var result = new ValidacaoUsuario().Validate(requestUsuario);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
