using MeChat.Common.Shared.Constants;
using MeChat.Common.UseCases.V1.Notification;
using MeChat.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeChat.Presentation.Controllers.V1;
public class NotificationController : ApiControllerBase
{
    public NotificationController(ISender sender) : base(sender)
    {
    }

    #region Get notifications
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetNotifications(int pageIndex)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppConstants.Configuration.Jwt.id)!.Value;
        var reqeust = new Query.GetNotifications(userId, pageIndex);
        var result = await sender.Send(reqeust);
        return Ok(result);
    }
    #endregion

}
