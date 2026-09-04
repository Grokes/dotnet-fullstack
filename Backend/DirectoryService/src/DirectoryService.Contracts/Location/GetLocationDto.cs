using DirectoryService.Contracts.Address;

namespace DirectoryService.Contracts.Location;

public record GetLocationDto(Guid Id, string Name, AddressDto Address, DateTime CreatedAt, DateTime UpdatedAt);