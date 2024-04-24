using MeChat.Common.UseCases.V1.Auth;
using MeChat.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeChat.Presentation.Controllers.V1;
public class AuthController : ApiControllerBase
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Query.Login login)
    {
        var result = await sender.Send(login);
        return Ok(result);
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody]RequestBodyModel.RefreshTokenRequest request)
    {
        Query.RefreshToken query = new Query.RefreshToken(HttpContext.Request.Headers.Authorization.ToString(), request.RefreshToken);
        var result = await sender.Send(query);
        return Ok(result);
    }

}
