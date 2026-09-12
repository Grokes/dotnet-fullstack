namespace DirectoryService.Contracts.Department;

public record CreateDepartmentDto(
    string Name,
    string Slug,
    IReadOnlyList<Guid> LocationsIds,
    Guid? ParentId = null);