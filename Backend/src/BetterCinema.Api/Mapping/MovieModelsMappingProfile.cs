using AutoMapper;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Domain.Entities;

namespace BetterCinema.Api.Mapping
{
    public class MovieModelsMappingProfile : Profile
    {
        public MovieModelsMappingProfile()
        {
            CreateMap<CreateMovieRequest, MovieEntity>();
            CreateMap<GetMovieResponse, MovieEntity>().ReverseMap();
        }
    }
}
