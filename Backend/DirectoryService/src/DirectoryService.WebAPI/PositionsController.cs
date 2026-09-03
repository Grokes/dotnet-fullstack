using DirectoryService.Contracts.Position;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class PositionsController : ControllerBase
    {
        [HttpGet("{positionId:guid}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid positionId,
            CancellationToken cancellationToken
        )
        {
            return Ok(new GetPositionDto(new Guid(), "name", DateTime.UtcNow, DateTime.UtcNow));
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken
        )
        {
            return Ok(Array.Empty<GetPositionDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePositionRequest request,
            CancellationToken cancellationToken
        )
        {
            return Created("", new Guid());
        }

        [HttpPut("{positionId:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid positionId,
            [FromBody] UpdatePositionRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok();
        }

        [HttpDelete("{positionId:guid}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid positionId,
            CancellationToken cancellationToken
        )
        {
            return Ok();
        }
    }
}
