using DirectoryService.Contracts.Department;
using FluentValidation;

namespace DirectoryService.Application.Departments;

public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDto>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Название департамента не может превышать 100 символов")
            .NotNull()
            .WithMessage("Название не должно быть null")
            .NotEmpty()
            .WithMessage("Название не должно быть пустым");
    }
}