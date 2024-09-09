﻿using MeChat.Common.Abstractions.Messages.DomainEvents;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;

namespace MeChat.Common.UseCases.V1.User;
public class Command
{
    public record AddUser(string Username, string Password) : ICommand;
    public record UpdateUser(Guid Id, string Username, string Password) : ICommand;
    public record DeleteUser(Guid Id) : ICommand;
    public record UpdateUserInfoRequestBody(string Fullname, IFormFile? Avatar);
    public record UpdateUserInfo(Guid Id, string Fullname, IFormFile? Avatar) : UpdateUserInfoRequestBody(Fullname, Avatar), ICommand;
}
