using AutoMapper;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;

namespace MyEdu.Service.Mappers
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}