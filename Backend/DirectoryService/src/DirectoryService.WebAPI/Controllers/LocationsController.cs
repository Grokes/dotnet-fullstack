using DirectoryService.Application.Locations;
using DirectoryService.Contracts.Location;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly LocationsService _locationsService;

    public LocationsController(LocationsService locationsService)
    {
        _locationsService = locationsService;
    }

    [HttpGet("{locationId:guid}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken
    )
    {
        var location = await _locationsService.GetByIdAsync(locationId, cancellationToken);

        return Ok(location);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        return Ok(await _locationsService.GetAllAsync(cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateLocationDto request,
        CancellationToken cancellationToken
    )
    {
        var locationId = await _locationsService.CreateAsync(request, cancellationToken);
        return Created("", locationId);
    }

    [HttpPut("{locationId:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid locationId,
        [FromBody] UpdateLocationDto request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _locationsService.UpdateAsync(locationId, request, cancellationToken));
    }

    [HttpDelete("{locationId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _locationsService.DeleteByIdAsync(locationId, cancellationToken));
    }
}