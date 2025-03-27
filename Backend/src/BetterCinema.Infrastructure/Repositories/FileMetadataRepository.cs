using BetterCinema.Domain.Entities;
using BetterCinema.Domain.Repositories;
using BetterCinema.Infrastructure.Data;

namespace BetterCinema.Infrastructure.Repositories;

public class FileMetadataRepository(CinemaDbContext context)
    : GenericRepository<FileMetadataEntity, int>(context), IFileMetadataRepository;