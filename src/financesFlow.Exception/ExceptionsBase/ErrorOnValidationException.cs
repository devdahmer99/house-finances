using System.Net;

namespace financesFlow.Exception.ExceptionsBase;
public class ErrorOnValidationException : financesFlowException
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Join("; ", errorMessages))
    {
        if (errorMessages == null || !errorMessages.Any())
        {
            throw new ArgumentException("Mensagens de erro não podem ser nulas ou vazias", nameof(errorMessages));
        }
        _errors = errorMessages;
    }

    public override List<string> BuscaErrors()
    {
        return _errors;
    }
}
