namespace BetterCinema.Application.FilesManagement.Interfaces;

public interface IFileUploadService
{
    Task<Result> UploadFileAsync(UploadFileRequest request);
    Task<Result<string>> GetSignedUrlAsync(GetSignedUrlRequest request);
}

public class UploadFileRequest
{
    public required string FileName { get; set; }
    public required string FolderName { get; set; }
    public required Stream Stream { get; set; }
}

public class GetSignedUrlRequest
{
    public required string FileName { get; set; }
    public required string FolderName { get; set; }
}