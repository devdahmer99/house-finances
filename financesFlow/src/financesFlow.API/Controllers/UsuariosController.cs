using financesFlow.Aplicacao.useCase.Usuarios.Criar;
using financesFlow.Comunicacao.Requests.Usuario;
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
    }
}
