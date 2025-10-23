using Application.Common;
using Application.DTOs;
using Application.Interfaces;
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
            try
            {
                var result = await _service.GetAllAsync(pageNumber, pageSize);
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                var result = await _service.GetSingleAsync(id);
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSingle(EmployeeAddEditDto dto)
        {
            try
            {
                var result = await _service.CreateSingleAsync(dto);
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBulk(List<EmployeeAddEditDto> dto)
        {
            try
            {
                var result = await _service.CreateBulkAsync(dto);
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeAddEditDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(dto);
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBulk([FromBody] List<int> ids)
        {
            try
            {
                var result = await _service.DeleteBulkAsync(ids);
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTemplateData()
        {
            try
            {
                var result = await _service.GetAllTemplateDataAsync();
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllGallary()
        {
            try
            {
                var result = await _service.GetAllGallaryAsync();
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAudits()
        {
            try
            {
                var result = await _service.GetAllAuditsAsync();
                var response = new RequestResponse
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Success",
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new RequestResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
                return StatusCode(500, response);
            }
        }
    }
}
