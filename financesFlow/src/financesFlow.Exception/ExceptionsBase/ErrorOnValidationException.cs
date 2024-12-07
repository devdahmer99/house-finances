namespace financesFlow.Exception.ExceptionsBase;
public class ErrorOnValidationException : financesFlowException
{
    public List<string> Errors { get; set; }
    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
