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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var userQuery = new Query.GetUserById(id);
        var result = await sender.Send(userQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] Command.AddUser user)
    {
        var result = await sender.Send(user);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] Command.UpdateUser user)
    {
        var userCommand = new Command.UpdateUser(id, user.Username, user.Password);
        var resut = await sender.Send(userCommand);
        return Ok(resut);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var userCommand = new Command.DeleteUser(id);
        var result = await sender.Send(userCommand);
        return Ok(result);
    }
}
