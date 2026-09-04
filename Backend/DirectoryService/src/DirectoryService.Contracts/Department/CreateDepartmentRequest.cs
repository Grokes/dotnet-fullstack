namespace DirectoryService.Contracts.Department;

public record CreateDepartmentRequest(string Name, string Slug, Guid? ParentId = null);