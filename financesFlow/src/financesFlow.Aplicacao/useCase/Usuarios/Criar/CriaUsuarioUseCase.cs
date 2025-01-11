using System.Runtime.CompilerServices;
using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses.Usuario;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Repositories;
using financesFlow.Dominio.Repositories.Usuarios;
using financesFlow.Dominio.Seguranca.Criptografia;
using financesFlow.Dominio.Seguranca.Tokens;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace financesFlow.Aplicacao.useCase.Usuarios.Criar
{
    public class CriaUsuarioUseCase : ICriarUsuarioUseCase
    {
        private readonly IRepositorioUsuarioSomenteEscrita _repositorio;
        private readonly IRepositorioUsuarioSomenteLeitura _repositorioLeitura;
        private readonly IUnidadeDeTrabalho _unidade;
        private readonly IMapper _mapper;
        private readonly IEncriptadorSenha _encriptador;
        private readonly IGerarTokenAcesso _gerarTokenAcesso;

        public CriaUsuarioUseCase(
            IEncriptadorSenha encriptador, IRepositorioUsuarioSomenteLeitura repositorioLeitura,
            IRepositorioUsuarioSomenteEscrita repositorio, IUnidadeDeTrabalho unidade,
            IMapper mapper, IGerarTokenAcesso gerarTokenAcesso)
        {
            _encriptador = encriptador;
            _repositorio = repositorio;
            _repositorioLeitura = repositorioLeitura;
            _unidade = unidade;
            _mapper = mapper;
            _gerarTokenAcesso = gerarTokenAcesso;
        }

        public async Task<ResponseCriaUsuarioJson> Execute(RequestCriaUsuarioJson request)
        {
            await Validate(request);

            var usuario = _mapper.Map<Usuario>(request);
            usuario.Senha = _encriptador.Encript(request.Senha);
            usuario.IdentificadorUsuario = Guid.NewGuid();

            await _repositorio.CriaUsuario(usuario);
            await _unidade.Commit();

            return new ResponseCriaUsuarioJson
            {
                Nome = usuario.Nome,
                Token = _gerarTokenAcesso.Generate(usuario),
            };
        }

        private async Task Validate(RequestCriaUsuarioJson requestUsuario)
        {
            var result = new ValidacaoRegistrarUsuario().Validate(requestUsuario);
            var existeEmail = await _repositorioLeitura.ExisteUsuarioAtivoComEmail(requestUsuario.Email);
            if (existeEmail)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_EXISTE));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
