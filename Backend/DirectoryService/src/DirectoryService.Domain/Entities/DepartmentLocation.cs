using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryService.Domain.Entities
{
    public class DepartmentLocation
    {
        public Guid Id { get; private set; }
        public Guid DepartmentId { get; private set; }
        public Guid LocationId { get; private set; }
        public bool IsPrimary { get; private set; } = false;

        public DepartmentLocation(Guid departmentId, Guid locationId, bool isPrimary)
        {
            ArgumentNullException.ThrowIfNull(departmentId);
            ArgumentNullException.ThrowIfNull(locationId);

            Id = Guid.CreateVersion7();
            DepartmentId = departmentId;
            LocationId = locationId;
            IsPrimary = isPrimary;
        }
    }
}
