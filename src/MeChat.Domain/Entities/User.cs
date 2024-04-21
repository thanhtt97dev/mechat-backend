﻿using MeChat.Domain.Abstractions.Entities;

namespace MeChat.Domain.Entities;
public class User : DomainEntity<Guid>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
