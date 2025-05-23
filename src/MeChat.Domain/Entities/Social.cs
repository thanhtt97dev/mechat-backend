﻿using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class Social : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<UserSocial>? UserSocials { get; set; }
}
