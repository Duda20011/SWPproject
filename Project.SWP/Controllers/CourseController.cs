using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Service;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;
        public CourseController(ICourseServices services)
        {
            _courseServices = services;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseModel courseModel)
        {
            var result = await _courseServices.CreateCourse(courseModel);
            return Ok(result);
        }
    }
}
