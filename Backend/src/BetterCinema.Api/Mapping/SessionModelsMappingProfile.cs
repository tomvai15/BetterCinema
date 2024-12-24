using AutoMapper;
using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Domain.Entities;

namespace BetterCinema.Api.Mapping
{
    public class SessionModelsMappingProfile : Profile
    {
        public SessionModelsMappingProfile()
        {
            CreateMap<CreateSessionRequest, SessionEntity>();
            CreateMap<UpdateSessionRequest, SessionEntity>();
            CreateMap<SessionEntity, GetSessionResponse>();
            CreateMap<SessionEntity, CreateSessionRequest>();
        }
    }
}
