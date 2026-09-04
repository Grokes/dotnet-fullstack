namespace DirectoryService.Contracts.Department;

public record UpdateDepartmentRequest(string Name, string Slug, Guid? ParentId);