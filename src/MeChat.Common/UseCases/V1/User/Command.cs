using MeChat.Common.Abstractions.Messages.DomainEvents;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;

namespace MeChat.Common.UseCases.V1.User;
public class Command
{
    public record AddUser(string Username, string Password) : ICommand;
    public record UpdateUser(Guid Id, string Username, string Password) : ICommand;
    public record DeleteUser(Guid Id) : ICommand;
    public record UpdateUserInfoRequestBody(string Fullname, IFormFile? Avatar, IFormFile? CoverPhoto);
    public record UpdateUserInfo(Guid Id, string Fullname, IFormFile? Avatar, IFormFile? CoverPhoto) : UpdateUserInfoRequestBody(Fullname, Avatar, CoverPhoto), ICommand;
    public record UpdateUserPasswordRequestBody(string Username, string? OldPassword, string NewPassword);
    public record UpdateUserPassword(Guid Id, string Username, string? OldPassword, string NewPassword): UpdateUserPasswordRequestBody(Username, OldPassword, NewPassword), ICommand;
    public record MakeUserFriendRelationship(Guid UserId, Guid? FriendId, int Status) : ICommand;
}
