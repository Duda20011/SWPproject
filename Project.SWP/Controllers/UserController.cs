using Microsoft.AspNetCore.Mvc;
using Services.Model;
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
        public async Task<IActionResult> Login([FromBody] LoginModel model)
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
    }
}
