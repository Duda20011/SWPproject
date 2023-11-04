using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Service;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialServices _materialServices;
        public MaterialController(IMaterialServices services)
        {
            _materialServices = services;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaterialModel materialModel)
        {
            var result = await _materialServices.CreateMaterial(materialModel);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageIndex = 1, int pageSize = 10)
        {
            var result = await _materialServices.GetAllMaterial(pageIndex, pageSize);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialById(int id, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _materialServices.GetMaterialById(id, pageIndex, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] MaterialResponse materialModel)
        {
            var result = await _materialServices.UpdateMaterial(id, materialModel);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _materialServices.RemoveMaterial(id);
            return Ok(result);
        }
    }
}