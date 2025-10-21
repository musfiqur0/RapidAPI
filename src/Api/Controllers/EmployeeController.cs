using Application.DTOs;
using Application.Interface;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 0, int pageSize = 0)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await _service.GetSingleAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSingle(EmployeeAddEditDto dto)
        {
            var result = await _service.CreateSingleAsync(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBulk(List<EmployeeAddEditDto> dto)
        {
            var result = await _service.CreateBulkAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeAddEditDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBulk([FromBody] List<int> ids)
        {
            var result = await _service.DeleteBulkAsync(ids);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTemplateData()
        {
            var result = await _service.GetAllTemplateDataAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGallary()
        {
            var result = await _service.GetAllGallaryAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAudits()
        {
            var result = await _service.GetAllAuditsAsync();
            return Ok(result);
        }
    }
}
