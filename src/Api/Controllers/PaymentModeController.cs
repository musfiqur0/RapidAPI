using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentModeController : ControllerBase
    {
        private readonly IPaymentModeService _service;

        public PaymentModeController(IPaymentModeService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 0, int pageSize = 0)
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

        [HttpGet]
        public async Task<IActionResult> GetSingle(int id)
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

        [HttpPost]
        public async Task<IActionResult> CreateSingle(PaymentModeAddEditDto dto)
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

        [HttpPost]
        public async Task<IActionResult> CreateBulk(List<PaymentModeAddEditDto> dto)
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

        [HttpPut]
        public async Task<IActionResult> Update(PaymentModeAddEditDto dto)
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            var response = new RequestResponse
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Success",
                Data = null
            };
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBulk([FromBody] List<int> ids)
        {
            var result = await _service.DeleteBulkAsync(ids);
            var response = new RequestResponse
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Success",
                Data = null
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTemplateData()
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

        [HttpGet]
        public async Task<IActionResult> GetAllGallary()
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

        [HttpGet]
        public async Task<IActionResult> GetAllAudits()
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
    }
}
