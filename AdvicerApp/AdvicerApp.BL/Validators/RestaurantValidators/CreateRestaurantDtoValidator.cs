using AdvicerApp.BL.DTOs.RestaurantDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.RestaurantValidators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Restaurant name cannot be empty")
            .MaximumLength(32)
            .WithMessage("Restaurant name can be maximum 32 characters");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description cannot be empty")
            .MaximumLength(512)
            .WithMessage("Description can be maximum 512 characters");

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .WithMessage("Adress cannot be empty")
            .MaximumLength(128)
            .WithMessage("Adress can be maximum 128 characters");

        RuleFor(x => x.Location)
            .NotNull()
            .NotEmpty()
            .WithMessage("Location cannot be empty")
            .MaximumLength(64)
            .WithMessage("Adress can be maximum 64 characters");

        RuleFor(x => x.Phone)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone cannot be empty")
            .Matches("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}")
            .WithMessage("It should be a phone number!")
            .MaximumLength(128)
            .WithMessage("Phone can be maximum 128 characters");
    }
}
