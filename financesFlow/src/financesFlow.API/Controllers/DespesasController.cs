using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;
using Microsoft.AspNetCore.Mvc;

namespace financesFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DespesasController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult RegistrarDespesa([FromBody] RequestDespesaJson requestDespesa)
    {
        try
        {
            var useCase = new RegistrarDespesaUseCase();
            var response = useCase.Execute(requestDespesa);

            return Created(string.Empty, response);

        } catch(ArgumentException ex)
        {
            //var responseError = new ResponseErrorsJson
            //{
            //    ErrorsMessages = ex.Message,
            //};
            //return BadRequest(responseError);
        } catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro Interno no Servidor");
        }
    }
}
