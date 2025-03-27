using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using BetterCinema.Application;
using BetterCinema.Application.FilesManagement.Interfaces;

namespace BetterCinema.Infrastructure.FileUpload;

public class FileUploadService : IFileUploadService
{
    private const int ExpiryTimeInMinutes = 1;

    public async Task<Result> UploadFileAsync(UploadFileRequest request)
    {
        try
        {
            var blobContainerClient = CreateBlobContainerClient();
            var blobClient = blobContainerClient.GetBlobClient(request.FileName);

            var options = new BlobUploadOptions();
            options.HttpHeaders = new BlobHttpHeaders();
            options.HttpHeaders.ContentType = "image/jpeg";
            options.Tags = new Dictionary<string, string>
            {
                { "Sealed", "false" },
                { "Content", "image" },
                { "Date", "2020-04-20" }
            };
            await blobClient.UploadAsync(request.Stream, options);

            return new Result { StatusCode = StatusCode.Ok };
        }
        catch (Exception ex)
        {
            return new Result { StatusCode = StatusCode.BadRequest, Message = ex.Message };
        }
    }

    public async Task<Result<string>> GetSignedUrlAsync(GetSignedUrlRequest request)
    {
        try
        {
            var blobContainerClient = CreateBlobContainerClient();
            var blobClient = blobContainerClient.GetBlobClient(request.FileName);

            if (!blobClient.CanGenerateSasUri)
            {
                return Result<string>.BadResult(StatusCode.InternalServerError,
                    "SAS is not supported on this blob storage.");
            }

            var sasBuilder = new BlobSasBuilder(BlobSasPermissions.Read,
                DateTimeOffset.UtcNow.AddMinutes(ExpiryTimeInMinutes));
            sasBuilder.BlobName = request.FileName;

            var url = blobClient.GenerateSasUri(sasBuilder).ToString();

            return Result<string>.OkResult(url);
        }
        catch (Exception ex)
        {
            return Result<string>.BadResult(StatusCode.BadRequest, ex.Message);
        }
    }

    private static BlobContainerClient CreateBlobContainerClient()
    {
        return new BlobContainerClient(
            new Uri("http://127.0.0.1:10000/devstoreaccount1/testcontainer"),
            new StorageSharedKeyCredential("devstoreaccount1",
                "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==")
        );
    }
}