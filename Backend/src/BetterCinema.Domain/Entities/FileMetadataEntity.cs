using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities;

public class FileMetadataEntity: IIdentifiable<int>
{
    public int Id { get; set; }
    public string FileName { get; set; }
}