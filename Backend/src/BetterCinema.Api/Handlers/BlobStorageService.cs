using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace BetterCinema.Api.Handlers;

public interface IBlobStorageService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName);
    string GetSecureUrl(string fileName, int expiryMinutes = 60);
}

public class BlobStorageService(IConfiguration configuration) : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient = new(configuration["AzureStorage:ConnectionString"]);
    private readonly string _containerName = configuration["AzureStorage:ContainerName"];

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = blobContainer.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream);
        return fileName; // Store only the filename, not the full URL
    }

    public string GetSecureUrl(string fileName, int expiryMinutes = 60)
    {
        var blobClient = _blobServiceClient.GetBlobContainerClient(_containerName).GetBlobClient(fileName);

        if (!blobClient.CanGenerateSasUri)
        {
            throw new InvalidOperationException("SAS is not supported on this blob storage.");
        }

        var sasBuilder = new BlobSasBuilder()
        {
            BlobName = fileName,
            Resource = "b",
            ExpiresOn = DateTime.UtcNow.AddMinutes(expiryMinutes)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }
}