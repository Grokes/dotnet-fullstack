namespace DirectoryService.Contracts.Position;

public record GetPositionDto(Guid Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);