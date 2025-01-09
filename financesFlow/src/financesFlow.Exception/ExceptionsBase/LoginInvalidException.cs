
using System.Net;

namespace financesFlow.Exception.ExceptionsBase
{
    public class LoginInvalidException : financesFlowException
    {
        public LoginInvalidException() : base(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS)
        {

        }

        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public override List<string> BuscaErrors()
        {
            return [Message];
        }
    }
}
