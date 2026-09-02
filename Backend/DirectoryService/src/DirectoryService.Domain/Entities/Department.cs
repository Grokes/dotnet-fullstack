using System.Text.RegularExpressions;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Entities
{
    public class Department
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Slug Slug { get; private set; } = Slug.Empty;
        public string Path { get; private set; } = string.Empty;
        public Guid? ParentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Department(string name, Slug slug, Department? parent = null)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            if (name.Length > 100)
                throw new ArgumentException("Имя превышает 100 символов.", nameof(name));

            Id = Guid.CreateVersion7();
            Name = name;
            Slug = slug;
            ParentId = parent?.Id;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Path = $"{parent?.Path}/{Slug}";
        }

        private Department()
        {
        }
    }
}
