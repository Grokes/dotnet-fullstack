using DirectoryService.Contracts.Location;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;


namespace DirectoryService.Application;

public class LocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<LocationsService> _logger;
    private readonly IValidator<CreateLocationRequest> _validator;

    public LocationsService(
        ILocationsRepository locationsRepository, 
        ILogger<LocationsService> logger, 
        IValidator<CreateLocationRequest> validator)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
        _validator = validator;
    }
    
    public async Task<Guid> Create(CreateLocationRequest locationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(locationDto, cancellationToken);
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
}