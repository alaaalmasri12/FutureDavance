using AutoMapper;
using Futureadvance.Repostiory.MappingProfileFolder;

namespace Futureadvance.Repostiory.Extinsionss
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
            });

            return config.CreateMapper();
        }
    }
}
