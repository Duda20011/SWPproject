using AutoMapper;
using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, LoginModel>().ReverseMap();
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }
    }
}
