using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryService.Contracts.Department
{
    public record CreateDepartmentRequest(string Name, string Slug, Guid? ParentId = null);
}