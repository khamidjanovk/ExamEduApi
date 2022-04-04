using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Courses;
using MyEdu.Service.DTOs;
using MyEdu.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IWebHostEnvironment env;

        public CoursesController(ICourseService courseService, IWebHostEnvironment env)
        {
            this.courseService = courseService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> PostCourseAsync([FromForm]CourseDto courseDto)
        {
            var course = await courseService.CreateAsync(courseDto);

            return StatusCode(course.Code ?? course.Error.Code.Value, course);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IQueryable<Course>>>> GetCoursesAsync([FromQuery] PaginationParams @params)
        {
            var courses = await courseService.GetAllAsync(@params);

            return StatusCode(courses.Code ?? courses.Error.Code.Value, courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> GetCourseAsync(long id)
        {
            var course = await courseService.GetAsync(p => p.Id == id);

            return StatusCode(course.Code ?? course.Error.Code.Value, course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> PutCourseAsync(long id, CourseDto courseDto)
        {
            var course = await courseService.UpdateAsync(id, courseDto);

            return StatusCode(course.Code ?? course.Error.Code.Value, course);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> DeleteCourseAsync(long id)
        {
            var course = await courseService.DeleteAsync(p =>p.Id == id);

            return StatusCode(course.Code ?? course.Error.Code.Value, course);
        }
    }
}
