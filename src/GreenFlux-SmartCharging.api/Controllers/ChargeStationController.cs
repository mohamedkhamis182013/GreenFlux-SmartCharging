using GreenFlux_SmartCharging.api.DtoValidators;
using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux_SmartCharging.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChargeStationController : ControllerBase
{
    private readonly IChargeStationService _chargeStationService;
    private readonly ChargeStationDtoValidators _chargeStationDtoValidators;
    public ChargeStationController(IChargeStationService chargeStationService)
    {
        _chargeStationService = chargeStationService;
        _chargeStationDtoValidators = new ChargeStationDtoValidators();
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<ChargeStationDto>>> GetAllAsync()
    {
        var chargeStations = 
            await _chargeStationService.GetAllAsync();
        return Ok(chargeStations);
    }

    [HttpGet("GetByGroupId/{groupId}")]

    public async Task<ActionResult<IEnumerable<ChargeStationDto>>> GetByGroupIdAsync(Guid groupId)
    {
        var chargeStations = 
            await _chargeStationService.GetByGroupIdAsync(groupId);
        return Ok(chargeStations);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<ChargeStationDto>> GetByIdAsync(Guid id)
    {
        var chargeStation = 
            await _chargeStationService.GetByIdAsync(id);
        return Ok(chargeStation);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateAsync(ChargeStationDto chargeStationDto)
    {
        var result =  _chargeStationDtoValidators.Validate(chargeStationDto);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _chargeStationService.AddAsync(chargeStationDto);
        return Ok();
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateAsync(ChargeStationDto chargeStationDto)
    {
        var result = _chargeStationDtoValidators.Validate(chargeStationDto);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _chargeStationService.UpdateAsync(chargeStationDto);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _chargeStationService.RemoveAsync(id);
        return Ok();
    }
}

