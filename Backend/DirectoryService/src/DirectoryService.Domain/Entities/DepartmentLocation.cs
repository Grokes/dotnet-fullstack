namespace DirectoryService.Domain.Entities;

public class DepartmentLocation
{
    public Guid Id { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid LocationId { get; private set; }
    public bool IsPrimary { get; private set; } = false;

    public DepartmentLocation(Guid departmentId, Guid locationId, bool isPrimary)
    {
        if (departmentId == Guid.Empty)
            throw new ArgumentException(
                "DepartmentId не может быть пустым.",
                nameof(departmentId)
            );

        if (locationId == Guid.Empty)
            throw new ArgumentException("LocationId не может быть пустым.", nameof(locationId));

        Id = Guid.CreateVersion7();
        DepartmentId = departmentId;
        LocationId = locationId;
        IsPrimary = isPrimary;
    }
        
}