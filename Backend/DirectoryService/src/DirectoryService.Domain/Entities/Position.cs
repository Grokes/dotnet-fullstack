using System.IO.Pipelines;

namespace DirectoryService.Domain.Entities
{
    public class Position
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Position(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            if (name.Length > 100)
                throw new ArgumentException("Имя превышает 100 символов.", nameof(name));
                
            Id = Guid.CreateVersion7();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
