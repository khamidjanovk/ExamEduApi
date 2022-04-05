using AutoMapper;
using MyEdu.Domain.Entities.Courses;
using MyEdu.Domain.Entities.Lessons;
using MyEdu.Domain.Entities.Parts;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;

namespace MyEdu.Service.Mappers
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Part, PartDto>().ReverseMap();
            CreateMap<CourseType, CourseTypeDto>().ReverseMap();
        }
    }
}