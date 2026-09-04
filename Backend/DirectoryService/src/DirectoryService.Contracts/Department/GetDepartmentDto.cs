namespace DirectoryService.Contracts.Department;

public record GetDepartmentDto(Guid Id, string Name, string Slug, string Path, Guid? ParentId, DateTime CreatedAt, DateTime UpdatedAt);