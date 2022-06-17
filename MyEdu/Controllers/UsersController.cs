using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Users;
using MyEdu.Service.DTOs;
using MyEdu.Service.Extensions;
using MyEdu.Service.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IWebHostEnvironment env;

        public UsersController(IUserService userService, IWebHostEnvironment env)
        {
            this.userService = userService;
            this.env = env;
        }

        [HttpPost]
        public async ValueTask<ActionResult<BaseResponse<User>>> PostCourseAsync([FromForm] UserDto userDto)
        {
            var user = await userService.CreateAsync(userDto);

            return StatusCode(user.Code ?? user.Error.Code.Value, user);
        }

        [HttpGet]
        public async ValueTask<ActionResult<BaseResponse<IQueryable<User>>>> GetCoursesAsync([FromQuery] PaginationParams @params)
        {
            var user = await userService.GetAllAsync(@params);

            return StatusCode(user.Code ?? user.Error.Code.Value, user);
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<BaseResponse<User>>> GetCourseAsync(long id)
        {
            var user = await userService.GetAsync(p => p.Id == id);

            return StatusCode(user.Code ?? user.Error.Code.Value, user);
        }

        [HttpPut("{id}")]
        public async ValueTask<ActionResult<BaseResponse<User>>> PutCourseAsync(long id, UserDto userDto)
        {
            var user = await userService.UpdateAsync(id, userDto);

            return StatusCode(user.Code ?? user.Error.Code.Value, user);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult<BaseResponse<bool>>> DeleteCourseAsync(long id)
        {
            var user = await userService.DeleteAsync(p => p.Id == id);

            return StatusCode(user.Code ?? user.Error.Code.Value, user);
        }

        [HttpGet("download")]
        public async ValueTask<IActionResult> DownloadUsers([FromQuery] PaginationParams @params)
        {
            var users = await userService.GetAllAsync(@params);

            var result = users.Data.ExportToExcel();

            return result;
        }
    }
}
