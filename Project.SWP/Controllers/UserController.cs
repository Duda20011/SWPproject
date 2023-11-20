using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Service;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;
        public UserController(IUserServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _services.Login(model);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            var result = await _services.CreateUser(model);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var result = await _services.GetUserById(id);
            return Ok(result);
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _services.GetAllUser();
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            var result = await _services.DeleteUser(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model, [FromQuery] int id)
        {
            var result = await _services.UpdateUser(model, id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> CheckUserBoughtCourse([FromQuery] int userId, [FromQuery] int courseId)
        {
            var result = await _services.CheckCourseUser(userId, courseId);
            return Ok(result);
        }
    }
}
