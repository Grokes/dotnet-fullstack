using DirectoryService.Contracts.Location;
using FluentValidation;

namespace DirectoryService.Application;

public class CreateLocationValidator: AbstractValidator<CreateLocationRequest>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Название локации не может быть пустым");
        RuleFor(x => x.Name).NotNull().WithMessage("Название локации не может быть NotNull");
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("Название локации не может превышать 100 символов");

        RuleFor(x => x.Address.Country).NotEmpty().WithMessage("Название страны не может быть пустым");
        RuleFor(x => x.Address.Country).NotNull().WithMessage("Название страны не может быть NotNull");
        RuleFor(x => x.Address.Country).MaximumLength(100).WithMessage("Название страны не может превышать 100 символов");

        RuleFor(x => x.Address.City).NotEmpty().WithMessage("Название города не может быть пустым");
        RuleFor(x => x.Address.City).NotNull().WithMessage("Название города не может быть NotNull");
        RuleFor(x => x.Address.City).MaximumLength(100).WithMessage("Название города не может превышать 100 символов");

        RuleFor(x => x.Address.Street).NotEmpty().WithMessage("Название улицы не может быть пустым");
        RuleFor(x => x.Address.Street).NotNull().WithMessage("Название улицы не может быть NotNull");
        RuleFor(x => x.Address.Street).MaximumLength(200).WithMessage("Название улицы не может превышать 200 символов");

        RuleFor(x => x.Address.Office).NotEmpty().WithMessage("Название офиса не может быть пустым");
        RuleFor(x => x.Address.Office).NotNull().WithMessage("Название офиса не может быть NotNull");
        RuleFor(x => x.Address.Office).MaximumLength(20).WithMessage("Название офиса не может превышать 20 символов");

    }
}