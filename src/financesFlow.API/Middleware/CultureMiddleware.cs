using System.Globalization;

namespace financesFlow.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    public CultureMiddleware(RequestDelegate request)
    {
        _requestDelegate = request;
    }
    public async Task Invoke(HttpContext context)
    {
        var linguagensSuportadas = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var idiomaSolicitado = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if(string.IsNullOrWhiteSpace(idiomaSolicitado) == false && linguagensSuportadas.Exists(ling => ling.Name.Equals(idiomaSolicitado)))
        {
            cultureInfo = new CultureInfo(idiomaSolicitado);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _requestDelegate(context);
    }
}
