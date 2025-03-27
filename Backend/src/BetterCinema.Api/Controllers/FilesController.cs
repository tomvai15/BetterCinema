using BetterCinema.Api.Contracts.Files;
using BetterCinema.Application.FilesManagement.Interfaces;
using BetterCinema.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BetterCinema.Api.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController(IFilePersistenceService filePersistenceService) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<UploadFileResponse> UploadImageAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var result = await filePersistenceService.UploadFileAsync(stream);
        return new UploadFileResponse
        {
            FileName = result
        };
    }

    [HttpGet("{fileName}")]
    public async Task<GetFileResponse> GetImageAsync(string fileName)
    {
        var result = await filePersistenceService.GetSignedUrlAsync(fileName);
        if (result == null)
        {
            throw new NotFoundException();
        }
        
        return new GetFileResponse
        {
            FileUrl = result
        };
    }
}