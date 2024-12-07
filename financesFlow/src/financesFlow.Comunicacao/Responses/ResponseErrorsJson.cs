namespace financesFlow.Comunicacao.Responses;
public class ResponseErrorsJson
{
    public List<string> ErrorMessage { get; set; }

    public ResponseErrorsJson(string errorMessage)
    {
        ErrorMessage = new List<string> { errorMessage };
    }

    public ResponseErrorsJson(List<string> errorMessages)
    {
        ErrorMessage = errorMessages;
    }
}
