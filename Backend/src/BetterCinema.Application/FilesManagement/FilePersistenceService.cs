using BetterCinema.Application.FilesManagement.Interfaces;
using BetterCinema.Domain.Entities;
using BetterCinema.Domain.Repositories;

namespace BetterCinema.Application.FilesManagement;

public class FilePersistenceService(
    IFileUploadService fileUploadService,
    IFileMetadataRepository fileMetadataRepository) : IFilePersistenceService
{
    private const string FolderName = "Pictures";
    private const string FileExtension = ".jpg";

    public async Task<string> UploadFileAsync(Stream fileStream)
    {
        var fileMetadata = new FileMetadataEntity
        {
            FileName = $"{Guid.NewGuid().ToString()}{FileExtension}",
        };
        fileMetadataRepository.Add(fileMetadata);
        await fileMetadataRepository.SaveChangesAsync();

        var result = await fileUploadService.UploadFileAsync(new UploadFileRequest
        {
            FileName = fileMetadata.FileName,
            FolderName = FolderName,
            Stream = fileStream,
        });

        if (result.IsSuccessful())
        {
            return fileMetadata.FileName;
        }

        fileMetadataRepository.Delete(fileMetadata);
        await fileMetadataRepository.SaveChangesAsync();

        throw new Exception();
    }

    public async Task<string?> GetSignedUrlAsync(string fileName)
    {
        var result = await fileUploadService.GetSignedUrlAsync(new GetSignedUrlRequest
        {
            FileName = fileName,
            FolderName = FolderName,
        });
        
        return result.Data;
    }
}