namespace financesFlow.Exception.ExceptionsBase;
public abstract class financesFlowException : SystemException
{
    protected financesFlowException(string message) : base(message)
    {}
}
