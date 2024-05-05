using MeChat.Common.UseCases.V1.Storage;
using MeChat.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeChat.Presentation.Controllers.V1;
public class StorageController : ApiControllerBase
{
    public StorageController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] Command.UploadFile command)
    {
        var result = await sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{fileName}")]
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        var command = new Command.DeleteFile(fileName);
        var result = await sender.Send(command);
        return Ok(result);
    }

}
