namespace financesFlow.Exception.ExceptionsBase;
public abstract class financesFlowException : SystemException
{
    protected financesFlowException(string message) : base(message)
    {}

    public abstract int StatusCode { get; }
    public abstract List<string> BuscaErrors();
}
