using AutoMapper;
using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Mapping
{
    public class UserModelsMappingProfile : Profile
    {
        public UserModelsMappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
