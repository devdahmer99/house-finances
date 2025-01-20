using System.Net;

namespace financesFlow.Exception.ExceptionsBase;
public class NotFoundException : financesFlowException
{
    public NotFoundException(string message) : base(message)
    {  }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> BuscaErrors()
    {
        return new List<string>() { Message };
    }
}
