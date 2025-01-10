using financesFlow.Aplicacao.useCase.Despesa.Atualiza;
using financesFlow.Aplicacao.useCase.Despesa.Busca;
using financesFlow.Aplicacao.useCase.Despesa.BuscaPorId;
using financesFlow.Aplicacao.useCase.Despesa.BuscaTotal;
using financesFlow.Aplicacao.useCase.Despesa.Deleta;
using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Comunicacao.Responses;
using financesFlow.Comunicacao.Responses.Despesa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financesFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DespesasController : ControllerBase
{

    [HttpPost("registradespesa")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize]
    public async Task<IActionResult> RegistrarDespesa([FromServices]IRegistrarDespesaUseCase useCase, [FromBody] RequestDespesaJson requestDespesa)
    {
      var response = await useCase.Execute(requestDespesa);

      return Created(string.Empty, response);    
    }

    [HttpGet("buscadespesas")]
    [ProducesResponseType(typeof(ResponseDespesasJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
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
    [Route("buscadespesa/{id}")]
    [ProducesResponseType(typeof(ResponseDespesaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscaDespesaPorId([FromRoute] long id, [FromServices] IBuscaDespesaPorIdUseCase useCase)
    {
        var resultado = await useCase.Execute(id);
        return Ok(resultado);
    }

    [HttpDelete]
    [Route("removedespesa/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletarDespesa([FromServices] IDeletaDespesaUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut]
    [Route("atualizadespesa/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaDespesa([FromServices] IAtualizaDespesaUseCase useCase, [FromRoute] long id, [FromBody] RequestDespesaJson request)
    {
        await useCase.Execute(id,request);
        return NoContent();
    }

    [HttpGet("totaldespesas")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscaTotalDespesas([FromServices] IBuscaTotalDespesasUseCase useCase)
    {
        var resultado = await useCase.Execute();
        if(!resultado.Equals(0))
        {
            return Ok(resultado);
        }

        return NoContent();
    }
}
