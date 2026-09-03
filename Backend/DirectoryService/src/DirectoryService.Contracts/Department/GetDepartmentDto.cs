using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryService.Contracts.Department
{
    public record GetDepartmentDto(Guid Id, string Name, string Slug, string Path, Guid? ParentId, DateTime CreatedAt, DateTime UpdatedAt);

}