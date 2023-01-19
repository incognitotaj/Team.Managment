using FluentValidation;

namespace Team.Application.Features.ProjectServers.Commands.UpdateProjectServer
{
    public class UpdateProjectServerValidator : AbstractValidator<UpdateProjectServerCommand>
    {
        public UpdateProjectServerValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{Title} is required")
                .NotNull()
                .MaximumLength(150).WithMessage("{Title} must not exceeds 150 characters");

            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("{Url} is required")
                .NotNull()
                .MaximumLength(256).WithMessage("{Url} must not exceeds 256 characters");

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{Username} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{Username} must not exceeds 50 characters");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{Password} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{Password} must not exceeds 50 characters");
        }
    }
}
