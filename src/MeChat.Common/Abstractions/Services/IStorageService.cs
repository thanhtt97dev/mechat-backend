using Microsoft.AspNetCore.Http;

namespace MeChat.Common.Abstractions.Services;
public interface IStorageService
{
    Task<byte[]> DownloadFileAsync(string fileName);
    Task<string> UploadFileAsync(IFormFile file, string fileName);
    Task DeleteFileAsync(string fileName, string? versionId = "");
}
