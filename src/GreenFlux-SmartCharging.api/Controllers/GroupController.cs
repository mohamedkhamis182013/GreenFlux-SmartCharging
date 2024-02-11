using GreenFlux_SmartCharging.api.DtoValidators;
using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux_SmartCharging.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly GroupDtoValidator _groupDtoValidator;
    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
        _groupDtoValidator = new GroupDtoValidator();
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetAllAsync()
    {
        var groups = await _groupService.GetAllAsync();
        return Ok(groups);
    }


    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<GroupDto>> GetByIdAsync(Guid id)
    {
        var group = await _groupService.GetByIdAsync(id);
        return Ok(group);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateAsync(GroupDto groupDto)
    {
        var result =  _groupDtoValidator.Validate(groupDto);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _groupService.AddAsync(groupDto);
        return Ok();
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateAsync(GroupDto groupDto)
    {
        var result = _groupDtoValidator.Validate(groupDto);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _groupService.UpdateAsync(groupDto);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _groupService.RemoveAsync(id);
        return Ok();
    }

}

