using DirectoryService.Contracts.Address;
using DirectoryService.Contracts.Location;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.WebAPI;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    [HttpGet("{locationId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken
    )
    {
        return Ok(
            new GetLocationDto(
                new Guid(),
                "Главный офис",
                new AddressDto("Россия", "Москва", "Ленина", "101"),
                DateTime.UtcNow,
                DateTime.UtcNow
            )
        );
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok(Array.Empty<GetLocationDto>());
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateLocationRequest request,
        CancellationToken cancellationToken
    )
    {
        return Created("", new Guid());
    }

    [HttpPut("{locationId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid locationId,
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok();
    }

    [HttpDelete("{locationId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken
    )
    {
        return Ok();
    }
}