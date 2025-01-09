using DocumentFormat.OpenXml.Presentation;
using financesFlow.Aplicacao.useCase.Usuarios.Login;
using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses;
using financesFlow.Comunicacao.Responses.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace financesFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseUsuarioRegistradoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(
            [FromServices] IFazLoginUsuario useCase,
            [FromBody] RequestLoginUsuarioJson requestLogin)
        {
            var response = await useCase.Execute(requestLogin);

            return Ok(response);
        }
    }
}
