using financesFlow.Comunicacao.Responses;
using financesFlow.Exception;
using financesFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace financesFlow.API.Filtros;

public class FiltroDeExcecao : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is financesFlowException)
        {
            HandleProjectException(context);
        } else
        {
            ThrowUnkowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var financeFlowException = (financesFlowException)context.Exception;
        var errorResponse = new ResponseErrorsJson(financeFlowException.BuscaErrors());
        context.HttpContext.Response.StatusCode = financeFlowException.StatusCode;
        context.Result = new ObjectResult(financeFlowException);
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorsJson(ResourceErrorMessages.UNKNOWN_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
