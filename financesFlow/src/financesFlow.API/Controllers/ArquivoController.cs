using financesFlow.Aplicacao.useCase.Arquivo;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace financesFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ArquivoController : Controller
{
    [HttpGet("/ObterDocumentoExcel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
   public async Task<IActionResult> ObterDocumentoExcel([FromServices] IGeraArquivoDespesaUseCase useCase, [FromHeader] DateOnly mes)
    {
        byte[] file = await useCase.GeraArquivo(mes);

        if(file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Octet, "arquivo.xlxs");
        }

        return NoContent();
    }
}
