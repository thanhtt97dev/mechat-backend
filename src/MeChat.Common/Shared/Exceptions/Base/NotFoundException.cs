using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChat.Common.Shared.Exceptions.Base;
public class NotFoundException : DomainException
{
    protected NotFoundException(string message) : base("Not Found", message)
    {
    }
}
