using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using financesFlow.Comunicacao.Requests;
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
      var useCase = new RegistrarDespesaUseCase();

      var response = useCase.Execute(requestDespesa);

      return Created(string.Empty, response);    
    }
}
