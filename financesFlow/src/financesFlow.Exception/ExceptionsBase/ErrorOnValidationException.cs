using System.Net;

namespace financesFlow.Exception.ExceptionsBase;
public class ErrorOnValidationException : financesFlowException
{
    private readonly List<string> _errors; 

    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        if (errorMessages == null || !errorMessages.Any())
        {
            throw new ArgumentException("Error messages cannot be null or empty", nameof(errorMessages));
        }
        _errors = errorMessages;
    }
    //public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    //{
    //    _errors = errorMessages;
    //}

    public override List<string> BuscaErrors()
    {
        return _errors;
    }
}
