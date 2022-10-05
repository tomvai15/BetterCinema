using AutoMapper;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Mapping
{
    public class TheaterModelsMappingProfile : Profile
    {
        public TheaterModelsMappingProfile()
        {
            CreateMap<CreateTheaterRequest, Theater>();
            CreateMap<UpdateTheaterRequest, Theater>();
        }
    }
}
