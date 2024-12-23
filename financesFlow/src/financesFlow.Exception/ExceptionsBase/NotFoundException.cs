using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace financesFlow.Exception.ExceptionsBase;
public class NotFoundException : financesFlowException
{
    public NotFoundException(string message) : base(message)
    {  }
}
