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
    private readonly IDepartmentsRepository _departmentRepository;
    private readonly ILocationsRepository _locationsRepository;

    public DepartmentService(IValidator<CreateDepartmentDto> validator,
        ILogger<DepartmentService> logger,
        IDepartmentsRepository repository,
        ILocationsRepository locationsRepository)
    {
        _validator = validator;
        _logger = logger;
        _departmentRepository = repository;
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

    public async Task<GetDepartmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);

        if (department is null)
        {
            _logger.LogError("Департамент не найден");
            throw new Exception("Департамент не найден");
        }

        var locationDto = new GetDepartmentDto(
            department.Id,
            department.Name,
            department.Slug.Value,
            department.Path,
            department.ParentId,
            department.CreatedAt,
            department.UpdatedAt
            );

        return locationDto;
    }

    public async Task<List<GetDepartmentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);

        return departments.Select(department => new GetDepartmentDto(
            department.Id,
            department.Name,
            department.Slug.Value,
            department.Path,
            department.ParentId,
            department.CreatedAt,
            department.UpdatedAt)).ToList();
    }

    public async Task<Guid> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _departmentRepository.DeleteAsync(id, cancellationToken);
    }

    
}
