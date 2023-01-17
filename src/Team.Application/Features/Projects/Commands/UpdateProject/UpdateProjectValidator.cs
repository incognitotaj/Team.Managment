using FluentValidation;

namespace Team.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Name} must not exceeds 100 characters");

            RuleFor(p => p.Detail)
                .MaximumLength(500).WithMessage("{Details} must not exceeds 500 characters");

            RuleFor(p => p.StartDate)
                .NotEmpty().WithMessage("{StartDate} is required")
                .NotNull();

            RuleFor(p => p.EndDate)
                .NotEmpty().WithMessage("{EndDate} is required")
                .NotNull();
        }
    }
}
