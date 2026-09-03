using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectoryService.Contracts.Address;

namespace DirectoryService.Contracts.Location
{
    public record UpdateLocationRequest(string Name, AddressDto Address);
}