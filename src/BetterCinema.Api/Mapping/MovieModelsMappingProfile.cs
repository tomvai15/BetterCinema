using AutoMapper;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Mapping
{
    public class MovieModelsMappingProfile : Profile
    {
        public MovieModelsMappingProfile()
        {
            CreateMap<CreateMovieRequest, Movie>();
            CreateMap<GetMovieResponse, Movie>().ReverseMap();
        }
    }
}
