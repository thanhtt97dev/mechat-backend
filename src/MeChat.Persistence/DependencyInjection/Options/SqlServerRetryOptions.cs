﻿using System.ComponentModel.DataAnnotations;

namespace MeChat.Persistence.DependencyInjection.Options;
public class SqlServerRetryOptions
{
    [Required, Range(5, 20)] 
    public int MaxRetryCount { get; init; }
    [Required, Timestamp] 
    public TimeSpan MaxRetryDelay { get; init; }
    public int[]? ErrorNumbersToAdd { get; init; }
}
