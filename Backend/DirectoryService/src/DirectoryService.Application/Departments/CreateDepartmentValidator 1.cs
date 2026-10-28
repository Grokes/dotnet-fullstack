using DirectoryService.Contracts.Department;
using FluentValidation;

namespace DirectoryService.Application.Departments;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Название департамента не может превышать 100 символов")
            .NotNull()
            .WithMessage("Название не должно быть null")
            .NotEmpty()
            .WithMessage("Название не должно быть пустым");


        RuleFor(x => x.Slug)
            .MaximumLength(100)
            .WithMessage("Слаг не может быть больше 100 символов")
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
            .WithMessage("Слаг должен содержать только строчные латинские буквы, цифры и одиночные дефисы.");
    }
}