using DirectoryService.Contracts.Department;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.WebAPI;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
    [HttpGet("{departmentsId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid departmentsId,
        CancellationToken cancellationToken
    )
    {
        return Ok(new GetDepartmentDto(new Guid(), "name", "slug", "path", new Guid(), DateTime.UtcNow, DateTime.UtcNow));
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken
    )
    {
        return Ok(Array.Empty<GetDepartmentDto>());
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDepartmentRequest request,
        CancellationToken cancellationToken
    )
    {
        return Created("", new Guid());
    }

    [HttpPut("{departmentsId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid departmentsId,
        [FromBody] UpdateDepartmentRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok();
    }

    [HttpDelete("{departmentsId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid departmentsId,
        CancellationToken cancellationToken
    )
    {
        return Ok();
    }
}