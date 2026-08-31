using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryService.Domain.Entities
{
    public class DepartmentPosition
    {
        public Guid Id { get; private set; }
        public Guid DepartmentId { get; private set; }
        public Guid PositionId { get; private set; }

        public DepartmentPosition(Guid departmentId, Guid positionId)
        {
            ArgumentNullException.ThrowIfNull(departmentId);
            ArgumentNullException.ThrowIfNull(positionId);

            Id = Guid.CreateVersion7();
            DepartmentId = departmentId;
            PositionId = positionId;
        }
    }
}