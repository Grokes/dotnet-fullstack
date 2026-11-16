using DirectoryService.Application.Departments;
using DirectoryService.Contracts.Department;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.WebAPI;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentsController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet("{departmentsId:guid}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid departmentId,
        CancellationToken cancellationToken
    )
    {
        var department = await _departmentService.GetByIdAsync(departmentId, cancellationToken);
        return Ok(department);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        CancellationToken cancellationToken
    )
    {
        return Ok(await _departmentService.GetAllAsync(cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDepartmentDto request,
        CancellationToken cancellationToken
    )
    {
        var departmentId = await _departmentService.CreateAsync(request, cancellationToken);
        return Created("", departmentId);
    }

    [HttpPut("{departmentsId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid departmentsId,
        [FromBody] UpdateDepartmentDto request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _departmentService.UpdateAsync(departmentsId, request, cancellationToken));
    }

    [HttpDelete("{departmentsId:guid}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid departmentId,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _departmentService.DeleteByIdAsync(departmentId, cancellationToken));
    }

    [HttpPost("{departmentsId:guid}/locations/{locationsId:guid}")]
    public async Task<IActionResult> AddLocationAsync(
        [FromRoute] Guid departmentsId,
        [FromRoute] Guid locationsId,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _departmentService.AddLocationAsync(departmentsId, locationsId, cancellationToken));
    }

    [HttpDelete("{departmentsId:guid}/locations/{locationsId:guid}")]
    public async Task<IActionResult> DeleteLocationAsync(
        [FromRoute] Guid departmentsId,
        [FromRoute] Guid locationsId,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _departmentService.DeleteLocationAsync(departmentsId, locationsId, cancellationToken));
    }
}