﻿using financesFlow.Aplicacao.useCase.Despesa.Busca;
using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using financesFlow.Comunicacao.Requests;
using Microsoft.AspNetCore.Mvc;

namespace financesFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DespesasController : ControllerBase
{
    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<IActionResult> BuscaDespesas([FromServices] IBuscaDespesaUseCase useCase)
    //{
    //    var response = await useCase.Execute();
    //}

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarDespesa([FromServices]IRegistrarDespesaUseCase useCase, [FromBody] RequestDespesaJson requestDespesa)
    {
      var response = await useCase.Execute(requestDespesa);

      return Created(string.Empty, response);    
    }
}
