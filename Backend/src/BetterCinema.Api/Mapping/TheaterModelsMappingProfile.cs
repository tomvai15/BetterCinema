using AutoMapper;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Domain.Entities;

namespace BetterCinema.Api.Mapping
{
    public class TheaterModelsMappingProfile : Profile
    {
        public TheaterModelsMappingProfile()
        {
            CreateMap<CreateTheaterRequest, TheaterEntity>();
            CreateMap<UpdateTheaterRequest, TheaterEntity>();
            CreateMap<TheaterEntity, GetTheaterResponse>();
        }
    }
}
