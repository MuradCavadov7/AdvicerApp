using AdvicerApp.BL.DTOs.MenuDtos;
using AdvicerApp.BL.DTOs.MenuItemDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.MenuValidator;

public class MenuUpdateDtoValidator : AbstractValidator<MenuUpdateDto>
{
    public MenuUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Menu's name cannot be emtpy")
            .MaximumLength(32)
            .WithMessage("Menu's name must be 32 characters");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Menu's description cannot be emtpy")
            .MaximumLength(512)
            .WithMessage("Menu's description must be 512 characters");
    }
}
