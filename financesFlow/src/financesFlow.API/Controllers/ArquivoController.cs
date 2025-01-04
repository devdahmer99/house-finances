using financesFlow.Aplicacao.useCase.Arquivo.Excel;
using financesFlow.Aplicacao.useCase.Arquivo.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace financesFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ArquivoController : Controller
{
    [HttpGet("/gerarRelatorioExcel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
   public async Task<IActionResult> geraRelatorioExcel([FromServices] IGeraArquivoExcelDespesaUseCase useCase, [FromHeader] DateOnly mes)
    {
        byte[] file = await useCase.GeraArquivo(mes);

        if(file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Octet, "Relatorio.xlxs");
        }

        return NoContent();
    }

    [HttpGet("/gerarRelatorioPdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> geraRelatorioPdf([FromServices] IGeraArquivoPdfDespesaUseCase useCase, [FromQuery] DateOnly mes)
    {
        byte[] file = await useCase.GeraArquivo(mes);
        if(file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Pdf, "Relatorio.pdf");
        }

        return NoContent();
    }
}
