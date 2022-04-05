using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Lessons;
using MyEdu.Service.DTOs;
using MyEdu.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService lessonService;
        private readonly IWebHostEnvironment env;

        public LessonsController(ILessonService lessonService, IWebHostEnvironment env)
        {
            this.lessonService = lessonService;
            this.env = env;
        }

        [HttpPost]
        public async ValueTask<ActionResult<BaseResponse<Lesson>>> PostCourseAsync([FromForm] LessonDto lessonDto)
        {
            var result = await lessonService.CreateAsync(lessonDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async ValueTask<ActionResult<BaseResponse<IQueryable<Lesson>>>> GetCoursesAsync([FromQuery] PaginationParams @params)
        {
            var result = await lessonService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<BaseResponse<Lesson>>> GetCourseAsync(long id)
        {
            var result = await lessonService.GetAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async ValueTask<ActionResult<BaseResponse<Lesson>>> PutCourseAsync(long id, LessonDto lessonDto)
        {
            var result = await lessonService.UpdateAsync(id, lessonDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult<BaseResponse<Lesson>>> DeleteCourseAsync(long id)
        {
            var result = await lessonService.DeleteAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
