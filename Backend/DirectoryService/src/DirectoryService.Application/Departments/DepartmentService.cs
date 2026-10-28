using DirectoryService.Application.Locations;
using DirectoryService.Contracts.Department;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Departments;

public class DepartmentService
{
    private readonly IValidator<CreateDepartmentDto> _createDtoValidator;
    private readonly IValidator<UpdateDepartmentDto> _updateDtoValidator;
    private readonly ILogger<DepartmentService> _logger;
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly ILocationsRepository _locationsRepository;

    public DepartmentService(
        IValidator<CreateDepartmentDto> createDtoValidator,
        IValidator<UpdateDepartmentDto> updateDtoValidator,
        ILogger<DepartmentService> logger,
        IDepartmentsRepository departmentsRepository,
        ILocationsRepository locationsRepository)
    {
        _createDtoValidator = createDtoValidator;
        _logger = logger;
        _departmentsRepository = departmentsRepository;
        _locationsRepository = locationsRepository;
        _updateDtoValidator = updateDtoValidator;
    }

    public async Task<Guid> CreateAsync(CreateDepartmentDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _createDtoValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Department? parent = null;

        if (request.ParentId is { } parentId)
        {
            parent = await _departmentsRepository.GetByIdAsync(parentId, cancellationToken);
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
            await _departmentsRepository.AddAsync(department, cancellationToken);
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

        await _departmentsRepository.AddWithLocationsAsync(department, departmentLocations, cancellationToken);
        _logger.LogInformation("Создан департамент {id}", department.Id);

        return department.Id;
    }

    public async Task<GetDepartmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var department = await _departmentsRepository.GetByIdAsync(id, cancellationToken);

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
        var departments = await _departmentsRepository.GetAllAsync(cancellationToken);

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
        return await _departmentsRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateAsync(Guid id, UpdateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var validationResult = await _updateDtoValidator.ValidateAsync(departmentDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var department = await _departmentsRepository.GetByIdAsync(id, cancellationToken);
        if (department is null)
        {
            throw new Exception("Департамента не существует");
        }

        department.ChangeName(departmentDto.Name);

        await _departmentsRepository.SaveAsync(cancellationToken);
        _logger.LogInformation("Департамент с id{location.Id} обновлен", department.Id);

        return department.Id;
    }

    public async Task<Guid> AddLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        var department = await _departmentsRepository.GetByIdAsync(departmentId, cancellationToken);
        if (department is null)
        {
            _logger.LogError("Департамент не найден");
            throw new Exception("Департамент не найден");
        }
        var location = await _locationsRepository.GetByIdAsync(locationId, cancellationToken);
        if(location is null)
        {
            _logger.LogError("Локация не найдена");
            throw new Exception("Локация не найдена");
        }

        var departmentLocationsIds = await _departmentsRepository.GetLocationsAsync(departmentId, cancellationToken);

        if (departmentLocationsIds.Contains(locationId))
        {
            _logger.LogError("Локация уже принадлежит департаменту");
            throw new Exception("Локация уже принадлежит департаменту");
        }

        return await _departmentsRepository.AddLocationAsync(departmentId, locationId, cancellationToken);
    }

    public async Task<Guid> DeleteLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        var department = await _departmentsRepository.GetByIdAsync(departmentId, cancellationToken);
        if (department is null)
        {
            _logger.LogError("Департамент не найден");
            throw new Exception("Департамент не найден");
        }

        var location = await _locationsRepository.GetByIdAsync(locationId, cancellationToken);
        if (location is null)
        {
            _logger.LogError("Локация не найдена");
            throw new Exception("Локация не найдена");
        }

        var departmentLocationsIds = await _departmentsRepository.GetLocationsAsync(departmentId, cancellationToken);

        if (!departmentLocationsIds.Contains(locationId))
        {
            _logger.LogError("Локация не принадлежит департаменту");
            throw new Exception("Локация не принадлежит департаменту");
        }

        return await _departmentsRepository.DeleteLocationAsync(departmentId, locationId, cancellationToken);
    }
}
