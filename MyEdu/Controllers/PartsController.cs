using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Parts;
using MyEdu.Service.DTOs;
using MyEdu.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IPartService partService;
        private readonly IWebHostEnvironment env;

        public PartsController(IPartService partService, IWebHostEnvironment env)
        {
            this.partService = partService;
            this.env = env;
        }

        [HttpPost]
        public async ValueTask<ActionResult<BaseResponse<Part>>> PostAsync([FromForm] PartDto partDto)
        {
            var result = await partService.CreateAsync(partDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async ValueTask<ActionResult<BaseResponse<IQueryable<Part>>>> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var parts = await partService.GetAllAsync(@params);

            return StatusCode(parts.Code ?? parts.Error.Code.Value, parts);
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<BaseResponse<Part>>> GetAsync(long id)
        {
            var part = await partService.GetAsync(p => p.Id == id);

            return StatusCode(part.Code ?? part.Error.Code.Value, part);
        }

        [HttpPut("{id}")]
        public async ValueTask<ActionResult<BaseResponse<Part>>> PutAsync(long id, PartDto partDto)
        {
            var part = await partService.UpdateAsync(id, partDto);

            return StatusCode(part.Code ?? part.Error.Code.Value, part);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult<BaseResponse<Part>>> DeleteCourseAsync(long id)
        {
            var part = await partService.DeleteAsync(p => p.Id == id);

            return StatusCode(part.Code ?? part.Error.Code.Value, part);
        }
    }
}
