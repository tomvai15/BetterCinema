using BetterCinema.Domain.Entities;
using BetterCinema.Domain.Repositories;
using BetterCinema.Infrastructure.Data;

namespace BetterCinema.Infrastructure.Repositories;

public class TheatersRepository(CinemaDbContext context)
    : GenericRepository<TheaterEntity, int>(context), ITheatersRepository;