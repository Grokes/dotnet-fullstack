using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryService.Contracts.Address
{
    public record AddressDto(string Country, string City, string Street, string Office);
}