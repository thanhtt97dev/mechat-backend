﻿namespace MeChat.Infrastucture.Service.DependencyInjection.Configurations;
public class EmailConfiguration
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? SmtpServer { get; set; }
    public int Port { get; set; }
}
