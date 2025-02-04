using AdvicerApp.BL.DTOs.UserDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.UserValidators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username cannot be empty.")
            .MaximumLength(32)
            .WithMessage("Username can be a maximum of 32 characters");

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .MaximumLength(128)
            .WithMessage("Email can be a maximum of 128 characters")
            .EmailAddress();

        RuleFor(x => x.Fullname)
            .NotNull()
            .NotEmpty()
            .WithMessage("Fullname cannot be empty")
            .MaximumLength(64)
            .WithMessage("Email can be a maximum of 128 characters");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password cannot be empty");

        RuleFor(x => x.RePassword)
            .NotNull()
            .NotEmpty()
            .WithMessage("Repeat Password cannot be empty")
            .Equal(x => x.Password)
            .WithMessage("Repeat password is not the same as Password");
    }
}
