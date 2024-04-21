using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MeChat.Presentation.Abstractions;
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApiControllerBase : ControllerBase
{
    protected readonly ISender sender;
    public ApiControllerBase(ISender sender)
    {
        this.sender = sender;
    }
}
