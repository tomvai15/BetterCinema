using AutoMapper;
using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Domain.Entities;

namespace BetterCinema.Api.Mapping
{
    public class UserModelsMappingProfile : Profile
    {
        public UserModelsMappingProfile()
        {
            CreateMap<CreateUserRequest, UserEntity>();
        }
    }
}
