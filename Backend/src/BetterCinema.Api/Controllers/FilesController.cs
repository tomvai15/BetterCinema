using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using BetterCinema.Api.Contracts.Files;
using BetterCinema.Application.FilesManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BetterCinema.Api.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController(IFilePersistenceService filePersistenceService) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<UploadFileResponse> UploadImage(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var result = await filePersistenceService.UploadFileAsync(stream);
        return new UploadFileResponse
        {
            FileName = result
        };
    }
    
    [HttpGet("{fileName}")]
    public async Task<GetFileResponse> GetImage(string fileName)
    {
        var result = await filePersistenceService.GetSignedUrl(fileName);
        return new GetFileResponse
        {
            FileUrl = result
        };
    }

    private async Task<IActionResult> ActionResult(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        await using var stream = file.OpenReadStream();

        var client1 = new BlobContainerClient(
            new Uri("http://127.0.0.1:10000/devstoreaccount1/testcontainer"),
            new StorageSharedKeyCredential("devstoreaccount1",
                "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==")
        );
        const string blobName = "BlobName";
        var blobClient = client1.GetBlobClient(blobName);

        
        BlobUploadOptions options = new BlobUploadOptions();
        
        options.HttpHeaders = new BlobHttpHeaders(); 
        options.HttpHeaders.ContentType = "image/jpeg";
        options.Tags = new Dictionary<string, string>
        {
            { "Sealed", "false" },
            { "Content", "image" },
            { "Date", "2020-04-20" }
        };
        await blobClient.UploadAsync(stream, options);

        var url = GetSecureUrl(blobClient, blobName);


        return Ok(url);

        var client = GetBlobServiceClient();
        var a = client.GetBlobContainerClient("BlobName");
        await UploadFromStreamAsync(a, stream);

        return Ok();
    }

    private string GetSecureUrl(BlobClient blobClient, string fileName, int expiryMinutes = 60)
    {
        
        if (!blobClient.CanGenerateSasUri)
        {
            throw new InvalidOperationException("SAS is not supported on this blob storage.");
        }

        var sasBuilder = new BlobSasBuilder(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(expiryMinutes));

        sasBuilder.BlobName = "BlobName";
        
        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }


    private BlobServiceClient GetBlobServiceClient()
    {
        BlobServiceClient client =
            new(
                "AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;AccountName=devstoreaccount1;BlobEndpoint=http://localhost:10000/devstoreaccount1;QueueEndpoint=http://localhost:10001/devstoreaccount1;TableEndpoint=http://localhost:10002/devstoreaccount1;");

        return client;
    }

    private static async Task UploadFromStreamAsync(
        BlobContainerClient containerClient,
        Stream fileStream)
    {
        try
        {
            const string blobName = "BlobName";
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(fileStream, true);
            fileStream.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}