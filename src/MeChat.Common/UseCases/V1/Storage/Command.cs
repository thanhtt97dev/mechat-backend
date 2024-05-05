using MeChat.Common.Abstractions.Messages;
using Microsoft.AspNetCore.Http;

namespace MeChat.Common.UseCases.V1.Storage;
public class Command
{
    public record UploadFile(IFormFile File, string FileName) : ICommand;

    public record DeleteFile(string fileName) : ICommand;
}
