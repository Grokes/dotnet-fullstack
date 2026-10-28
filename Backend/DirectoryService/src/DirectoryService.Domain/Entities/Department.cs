using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Entities;

public class Department
{
    public Guid Id { get; }
    public string Name { get; private set; } = string.Empty;
    public Slug Slug { get; }
    public string Path { get; } = string.Empty;
    public Guid? ParentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Department(string name, Slug slug, Department? parent = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length > 100)
            throw new ArgumentException("Имя превышает 100 символов.", nameof(name));

        Id = Guid.CreateVersion7();
        Name = name;
        Slug = slug;
        ParentId = parent?.Id;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Path = $"{parent?.Path}/{Slug.Value}";
    }

    public void ChangeName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length > 100)
            throw new ArgumentException("Имя превышает 100 символов.", nameof(name));

        Name = name;
    }


    private Department()
    {
    }
}