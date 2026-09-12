using DirectoryService.Application.Locations;
using DirectoryService.Contracts.Department;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Departments;

public class DepartmentService
{
    private readonly IValidator<CreateDepartmentDto> _validator;
    private readonly ILogger<DepartmentService> _logger;
    private readonly IDepartmentsRepostitory _departmentRepository;
    private readonly ILocationsRepository _locationsRepository;

    public DepartmentService(IValidator<CreateDepartmentDto> validator,
        ILogger<DepartmentService> logger,
        IDepartmentsRepostitory repostitory,
        ILocationsRepository locationsRepository)
    {
        _validator = validator;
        _logger = logger;
        _departmentRepository = repostitory;
        _locationsRepository = locationsRepository;
    }

    public async Task<Guid> CreateAsync(CreateDepartmentDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Department? parent = null;

        if (request.ParentId is { } parentId)
        {
            parent = await _departmentRepository.GetByIdAsync(parentId, cancellationToken);
            if (parent is null)
            {
                _logger.LogError("Родитель {ParentId} не найден в БД", parentId);
                throw new Exception("Родитель не найден");
            }
        }

        var department = new Department(
            request.Name,
            new Slug(request.Slug),
            parent);

        if (request.LocationsIds.Count == 0)
        {
            await _departmentRepository.AddAsync(department, cancellationToken);
            _logger.LogInformation("Создан департамент {id}", department.Id);
            return department.Id;
        }

        var uniqueLocationIds = request.LocationsIds.Distinct().ToArray();
        var isLocationsExist = await _locationsRepository.IsAllExistAsync(uniqueLocationIds, cancellationToken);

        if (!isLocationsExist)
        {
            _logger.LogError("Не все локации существуют в БД");
            throw new Exception("Не все локации существуют в БД");
        }

        var departmentLocations = uniqueLocationIds
            .Select(locationId => new DepartmentLocation(department.Id, locationId)).ToList();

        await _departmentRepository.AddWithLocationsAsync(department, departmentLocations, cancellationToken);
        _logger.LogInformation("Создан департамент {id}", department.Id);

        return department.Id;
    }
}