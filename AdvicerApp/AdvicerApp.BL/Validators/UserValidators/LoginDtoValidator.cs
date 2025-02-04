using AdvicerApp.BL.DTOs.UserDtos;
using FluentValidation;

namespace AdvicerApp.BL.Validators.UserValidators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UsernameOrEmail)
            .NotNull()
            .NotEmpty()
            .WithMessage("username or email cannot be empty")
            .MaximumLength(128)
            .WithMessage("Username or email maximum 128 characters");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("The password field cannot be empty");
    }
}
