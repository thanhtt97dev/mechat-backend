using Asp.Versioning;
using MeChat.Common.UseCases.V1.User;
using MeChat.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MeChat.Presentation.Controllers.V1;

[ApiVersion(1)]
public class UserController : ApiControllerBase
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] Command.AddUser user)
    {
        var result = await sender.Send(user);
        return Ok(result);
    }
}
