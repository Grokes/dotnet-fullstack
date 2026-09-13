using DirectoryService.Contracts.Address;
using DirectoryService.Contracts.Location;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Locations;

public class LocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<LocationsService> _logger;
    private readonly IValidator<CreateLocationDto> _createDtoValidator;
    private readonly IValidator<UpdateLocationDto> _updateDtoValidator;

    public LocationsService(
        ILocationsRepository locationsRepository,
        ILogger<LocationsService> logger,
        IValidator<CreateLocationDto> validator,
        IValidator<UpdateLocationDto> updateDtoValidator)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
        _createDtoValidator = validator;
        _updateDtoValidator = updateDtoValidator;
    }

    public async Task<Guid> CreateAsync(CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _createDtoValidator.ValidateAsync(locationDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Guid checkName = await _locationsRepository.GetIdByNameAsync(locationDto.Name, cancellationToken);
        if (checkName != Guid.Empty)
        {
            throw new Exception("Это имя уже использовано");
        }

        var location = new Location(
            locationDto.Name,
            new Address(
                locationDto.Address.Country,
                locationDto.Address.City,
                locationDto.Address.Street,
                locationDto.Address.Office));

        await _locationsRepository.AddAsync(location, cancellationToken);
        _logger.LogInformation("Создана локация с id{location.Id}", location.Id);

        return location.Id;
    }

    public async Task<GetLocationDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var location = await _locationsRepository.GetByIdAsync(id, cancellationToken);

        if (location is null)
        {
            _logger.LogError("Локация не найдена");
            throw new Exception("Локация не найдена");
        }

        var locationDto = new GetLocationDto(
            location.Id,
            location.Name,
            new AddressDto(
                location.Address.Country,
                location.Address.City,
                location.Address.Street,
                location.Address.Office),
            location.CreatedAt,
            location.UpdatedAt);

        return locationDto;
    }

    public async Task<List<GetLocationDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var locations = await _locationsRepository.GetAllAsync(cancellationToken);

        return locations.Select(x => new GetLocationDto(
            x.Id,
            x.Name,
            new AddressDto(
                x.Address.Country,
                x.Address.City,
                x.Address.Street,
                x.Address.Office),
            x.CreatedAt,
            x.UpdatedAt)).ToList();
    }

    public async Task<Guid> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _locationsRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateAsync(Guid id, UpdateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _updateDtoValidator.ValidateAsync(locationDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var location = await _locationsRepository.GetByIdAsync(id, cancellationToken);
        if (location is null)
        {
            throw new Exception("Локации не существует");
        }

        var checkName = await _locationsRepository.GetIdByNameAsync(locationDto.Name, cancellationToken);
        if (checkName != Guid.Empty)
        {
            throw new Exception("Это имя уже использовано");
        }

        location.ChangeName(locationDto.Name);
        location.ChangeAddress(
            new Address
            (locationDto.Address.Country,
                locationDto.Address.City,
                locationDto.Address.Street,
                locationDto.Address.Office
            ));

        await _locationsRepository.SaveAsync(cancellationToken);
        _logger.LogInformation("локация с id{location.Id} обновлена", location.Id);

        return location.Id;
    }
}