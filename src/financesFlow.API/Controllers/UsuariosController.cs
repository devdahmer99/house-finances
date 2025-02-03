using financesFlow.Aplicacao.useCase.Usuarios.Atualizar;
using financesFlow.Aplicacao.useCase.Usuarios.Criar;
using financesFlow.Aplicacao.useCase.Usuarios.Delete;
using financesFlow.Aplicacao.useCase.Usuarios.MudaSenha;
using financesFlow.Aplicacao.useCase.Usuarios.Profile;
using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses;
using financesFlow.Comunicacao.Responses.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financesFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpPost("registrausuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriaUsuario([FromServices] ICriarUsuarioUseCase useCase, [FromBody] RequestCriaUsuarioJson requestUsuario)
        {
            var resultado = await useCase.Execute(requestUsuario);
            if(resultado != null)
            {
                return Created(string.Empty, resultado);
            }

            return BadRequest();
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestAtualizaUsuarioJson request)
        {
            await useCase.Execute(request);

            return NoContent();
        }

        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IMudaSenhaUseCase useCase,
            [FromBody] RequestMudaSenhaJson request)
        {
            await useCase.Execute(request);

            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProfile([FromServices] IDeleteUserAccountUseCase useCase)
        {
            await useCase.Execute();

            return NoContent();
        }
    }  
}
