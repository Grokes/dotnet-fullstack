using DirectoryService.Contracts.Address;

namespace DirectoryService.Contracts.Location;

public record UpdateLocationRequest(string Name, AddressDto Address);