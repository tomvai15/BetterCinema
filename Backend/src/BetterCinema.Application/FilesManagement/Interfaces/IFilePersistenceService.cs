namespace BetterCinema.Application.FilesManagement.Interfaces;

public interface IFilePersistenceService
{
    Task<string> UploadFileAsync(Stream fileStream);
    Task<string?> GetSignedUrlAsync(string fileName);
}