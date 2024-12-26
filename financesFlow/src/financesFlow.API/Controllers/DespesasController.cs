using financesFlow.Aplicacao.useCase.Despesa.Busca;
using financesFlow.Aplicacao.useCase.Despesa.BuscaPorId;
using financesFlow.Aplicacao.useCase.Despesa.Deleta;
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
    public async Task<IActionResult> RegistrarDespesa([FromServices]IRegistrarDespesaUseCase useCase, [FromBody] RequestDespesaJson requestDespesa)
    {
      var response = await useCase.Execute(requestDespesa);

      return Created(string.Empty, response);    
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseDespesasJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscaTodasDespesas([FromServices] IBuscaDespesaUseCase useCase)
    {
        var response = await useCase.Execute();

        if(response.Despesas.Count != 0)
        {
            return Ok(response);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseDespesaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscaDespesaPorId([FromRoute] long id, [FromServices] IBuscaDespesaPorIdUseCase useCase)
    {
        
        var resultado = await useCase.Execute(id);

        return Ok(resultado);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletarDespesa([FromServices] IDeletaDespesaUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);
        return NoContent();
    }

}
