﻿using Asp.Versioning;
using MeChat.Common.Shared.Extentions;
using MeChat.Common.UseCases.V1.User;
using MeChat.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeChat.Presentation.Controllers.V1;

[ApiVersion(1)]
public class UserController : ApiControllerBase
{
    public UserController(ISender sender) : base(sender)
    {
    }

    #region Test
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var accessToken = HttpContext.Request.Headers.Authorization.ToString().Replace(JwtBearerDefaults.AuthenticationScheme, string.Empty).Trim();
        var user = new Query.GetUserById(id, accessToken);
        var result = await sender.Send(user);
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUsers(
        string? searchTerm,
        string? sortColumnWithOrders = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var userQuery = new Query.GetUsers(searchTerm, SortOrderExtention.Convert(sortColumnWithOrders), pageIndex, pageSize);
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
    #endregion

    #region Update User info
    [HttpPut("info/{id:guid}")]
    public async Task<IActionResult> UpdateUserInfo(Guid id, [FromForm] Command.UpdateUserInfoRequestBody request)
    {
        var updateUserInfo = new Command.UpdateUserInfo(id, request.Fullname, request.Avatar, request.CoverPhoto);
        var resut = await sender.Send(updateUserInfo);
        return Ok(resut);
    }
    #endregion

    #region Update user's password
    [HttpPut("password/{id:guid}")]
    public async Task<IActionResult> UpdateUserPassword(Guid id, Command.UpdateUserPasswordRequestBody request)
    {
        var updateUserPassword = new Command.UpdateUserPassword(id, request.Username, request.OldPassword, request.NewPassword);
        var result = await sender.Send(updateUserPassword);
        return Ok(result);
    }
    #endregion

    #region Get User's info by id, username or email
    [HttpGet("info")]
    public async Task<IActionResult> GetUserPublicInfo(string key, Guid? id) 
    {
        var userPublicInfo = new Query.GetUserPublicInfo(key, id);
        var result = await sender.Send(userPublicInfo);
        return Ok(result);
    }
    #endregion

    #region Make user's friend relationship
    [HttpPost("makefriend")]
    public async Task<IActionResult> MakeUserFriendRelationship(Command.MakeUserFriendRelationship request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    #endregion
}
