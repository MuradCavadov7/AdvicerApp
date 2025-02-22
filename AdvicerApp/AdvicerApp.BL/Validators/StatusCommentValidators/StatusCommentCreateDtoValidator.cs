using AdvicerApp.BL.DTOs.StatusCommentDto;
using FluentValidation;

namespace AdvicerApp.BL.Validators.StatusCommentValidators;

public class StatusCommentCreateDtoValidator : AbstractValidator<StatusCommentCreateDto>
{
    public StatusCommentCreateDtoValidator()
    {
        RuleFor(x => x.Content)
          .NotNull()
          .NotEmpty()
          .WithMessage("Comment cannot be empty")
          .MaximumLength(512)
          .WithMessage("Comment must be 512 characters");
    }
}
