using AutoMapper;
using Futureadvance.Core.Models;
using Futureadvance.Repostiory.Models;

namespace Futureadvance.Repostiory.MappingProfileFolder
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {

            CreateMap<ApplicationUser, Localuser>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
