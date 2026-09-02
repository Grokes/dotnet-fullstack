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
             if (departmentId == Guid.Empty)
                throw new ArgumentException(
                    "DepartmentId не может быть пустым.",
                    nameof(departmentId)
                );

            if (positionId == Guid.Empty)
                throw new ArgumentException("PositionId не может быть пустым.", nameof(positionId));

            Id = Guid.CreateVersion7();
            DepartmentId = departmentId;
            PositionId = positionId;
        }
    }
}