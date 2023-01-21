using AutoMapper;
using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Mapping
{
    public class SessionModelsMappingProfile : Profile
    {
        public SessionModelsMappingProfile()
        {
            CreateMap<CreateSessionRequest, Session>();
            CreateMap<UpdateSessionRequest, Session>();
            CreateMap<Session, GetSessionResponse>();
            CreateMap<Session, CreateSessionRequest>();
        }
    }
}
