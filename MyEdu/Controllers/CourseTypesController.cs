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
    public class CourseTypesController : ControllerBase
    {
        private readonly ICourseTypeService courseTypeService;
        public CourseTypesController(ICourseTypeService courseTypeService)
        {
            this.courseTypeService = courseTypeService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CourseType>>> CreateAsync(CourseTypeDto courseTypeDto)
        {
            var result = await courseTypeService.CreateAsync(courseTypeDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IQueryable<CourseType>>>> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var result = await courseTypeService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CourseType>>> GetAsync(long id)
        {
            var result = await courseTypeService.GetAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<CourseType>>> PutCourseAsync(long id, CourseTypeDto courseTypeDto)
        {
            var result = await courseTypeService.UpdateAsync(id, courseTypeDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<CourseType>>> DeleteCourseAsync(long id)
        {
            var result = await courseTypeService.DeleteAsync(p => p.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
