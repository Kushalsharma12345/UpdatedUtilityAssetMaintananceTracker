using Microsoft.AspNetCore.Mvc;
using MaintenanceService1.DTOs;
using MaintenanceService1.Services;

namespace MaintenanceService1.Controllers;

[ApiController]
[Route("api/maintenance-plans")]
public class MaintenancePlansController : ControllerBase
{
    private readonly MaintenanceService _service;

    public MaintenancePlansController(MaintenanceService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMaintenancePlanDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? assetId)
    {
        var result = await _service.GetAsync(assetId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateMaintenancePlanDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
            return NotFound("Plan not found");

        return Ok("Updated successfully");
    }
}