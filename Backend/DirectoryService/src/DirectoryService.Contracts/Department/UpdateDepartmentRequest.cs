using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryService.Contracts.Department
{
    public record UpdateDepartmentRequest(string Name, string Slug, Guid? ParentId);
}