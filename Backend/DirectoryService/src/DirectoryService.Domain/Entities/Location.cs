using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Entities
{
    public class Location
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public Address Address { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Location(string name, Address address)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            if (name.Length > 100)
                throw new ArgumentException("Имя превышает 100 символов.", nameof(name));

            Id = Guid.CreateVersion7();
            Name = name;
            Address = address;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

        }

        public void ChangeName(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            if (name.Length > 100)
                throw new ArgumentException("Имя превышает 100 символов.", nameof(name));

            Name = name;
        }

        public void ChangeAddress(Address address)
        {
            Address = address;
        }
    }
}
