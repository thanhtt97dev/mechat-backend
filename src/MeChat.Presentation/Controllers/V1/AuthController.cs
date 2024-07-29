﻿using MeChat.Common.Constants;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeChat.Presentation.Controllers.V1;
public class AuthController : ApiControllerBase
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(Query.SignIn signIn)
    {
        var result = await sender.Send(signIn);
        return Ok(result);
    }

    [HttpPost("SignInByGoogle")]
    public async Task<IActionResult> SignInByGoogle(Query.SignInByGoogle signInByGoogle)
    {
        var result = await sender.Send(signInByGoogle);
        return Ok(result);
    } 

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody]RequestBodyModel.RefreshTokenRequest request)
    {
        var accessToken = HttpContext.Request.Headers.Authorization.ToString().Replace(JwtBearerDefaults.AuthenticationScheme, string.Empty).Trim();
        var userId = HttpContext.Request.Headers.GetCommaSeparatedValues(AppConstants.AppConfigs.RequestHeader.USER_ID).FirstOrDefault();
        Query.RefreshToken query = new (accessToken, request.RefreshToken, userId);
        var result = await sender.Send(query);
        return Ok(result);
    }

}
