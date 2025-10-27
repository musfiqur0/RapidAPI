using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DeductionController : ControllerBase
{
    private readonly IDeductionService _service;

    public DeductionController(IDeductionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 0, int pageSize = 0)
    {
        var result = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetSingle(int id)
    {
        var result = await _service.GetSingleAsync(id);
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }

    [HttpPost]
    public async Task<IActionResult> CreateSingle(DeductionAddEditDto dto)
    {
        var result = await _service.CreateSingleAsync(dto);
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }

    [HttpPost]
    public async Task<IActionResult> CreateBulk(List<DeductionAddEditDto> dto)
    {
        var result = await _service.CreateBulkAsync(dto);
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }

    [HttpPut]
    public async Task<IActionResult> Update(DeductionAddEditDto dto)
    {
        var result = await _service.UpdateAsync(dto);
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = null });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBulk([FromBody] List<int> ids)
    {
        var result = await _service.DeleteBulkAsync(ids);
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = null });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTemplateData()
    {
        var result = await _service.GetAllTemplateDataAsync();
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGallary()
    {
        var result = await _service.GetAllGallaryAsync();
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAudits()
    {
        var result = await _service.GetAllAuditsAsync();
        return Ok(new RequestResponse { IsSuccess = true, StatusCode = 200, Message = "Success", Data = result });
    }
}