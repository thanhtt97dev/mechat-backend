using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChat.Common.Shared.Exceptions.Base;
public class BadRequestException : DomainException
{
    protected BadRequestException(string message) : base("Bad Request", message)
    {
    }
}
