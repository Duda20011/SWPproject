using AutoMapper;
using Services.Commons;

namespace Services.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }
    }
}
