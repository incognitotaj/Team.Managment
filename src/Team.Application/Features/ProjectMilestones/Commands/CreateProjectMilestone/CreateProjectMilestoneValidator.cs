using FluentValidation;

namespace Team.Application.Features.ProjectMilestones.Commands.CreateProjectMilestone
{
    public class CreateProjectMilestoneValidator : AbstractValidator<CreateProjectMilestoneCommand>
    {
        public CreateProjectMilestoneValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{Title} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Title} must not exceeds 100 characters");

            RuleFor(p => p.Detail)
                .NotEmpty().WithMessage("{Detail} is required")
                .NotNull()
                .MaximumLength(500).WithMessage("{Detail} must not exceeds 500 characters");

            RuleFor(p => p.FromDate)
                .NotEmpty().WithMessage("{FromDate} is required")
                .NotNull();
        }
    }
}
