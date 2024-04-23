using MeChat.Common.UseCases.V1.Auth;
using MeChat.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChat.Presentation.Controllers.V1;
public class AuthController : ApiControllerBase
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(Query.Login login)
    {
        var result = await sender.Send(login);
        return Ok(result);
    }
}
