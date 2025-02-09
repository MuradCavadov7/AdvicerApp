using AdvicerApp.BL.DTOs.MenuItemDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.MeniItemValidators;

public class MenuItemCreateDtoValidator : AbstractValidator<MenuItemCreateDto>
{
    public MenuItemCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Item name cannot be empty")
            .MaximumLength(50)
            .WithMessage("Item name can be maximum 50 characters ");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Item description cannot be empty")
            .MaximumLength(512)
            .WithMessage("Item description can be maximum 512 characters ");

        RuleFor(x => x.Price)
            .NotNull()
            .NotEmpty()
            .WithMessage("Item price cannot be empty");  
    }
}
