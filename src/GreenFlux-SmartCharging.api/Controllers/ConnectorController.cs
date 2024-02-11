using GreenFlux_SmartCharging.api.DtoValidators;
using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux_SmartCharging.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConnectorController : ControllerBase
{
    private readonly IConnectorService _connectorService;
    private readonly ConnectorDtoValidator _connectorDtoValidator;
    public ConnectorController(IConnectorService connectorService)
    {
        _connectorService = connectorService;
        _connectorDtoValidator = new ConnectorDtoValidator();
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<ConnectorDto>>> GetAllConnectorsAsync()
    {
        var connectors =
            await _connectorService.GetAllAsync();
        return Ok(connectors);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<ConnectorDto>> GetByIdAsync(int id)
    {
        var connector =
            await _connectorService.GetByIdAsync(id);
        return Ok(connector);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateAsync(ConnectorDto connector)
    {
        var result = _connectorDtoValidator.Validate(connector);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _connectorService.AddAsync(connector);
        return Ok();
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateAsync(ConnectorDto connectorDto)
    {
        var result = _connectorDtoValidator.Validate(connectorDto);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _connectorService.UpdateAsync(connectorDto);
        return Ok();
    }
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> RemoveAsync( int id)
    {
        await _connectorService.RemoveAsync(id);
        return Ok();
    }
}

