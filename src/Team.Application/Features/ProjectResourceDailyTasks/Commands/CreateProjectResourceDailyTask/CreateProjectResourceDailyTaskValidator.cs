using FluentValidation;

namespace Team.Application.Features.ProjectResourceDailyTasks.Commands.CreateProjectResourceDailyTask
{
    public class CreateProjectResourceDailyTaskValidator : AbstractValidator<CreateProjectResourceDailyTaskCommand>
    {
        public CreateProjectResourceDailyTaskValidator()
        {
            RuleFor(p => p.Title)
               .NotEmpty().WithMessage("{Title} is required")
               .NotNull()
               .MaximumLength(150).WithMessage("{Title} must not exceeds 150 characters");

            RuleFor(p => p.Description)
              .NotEmpty().WithMessage("{Description} is required")
              .NotNull()
              .MaximumLength(500).WithMessage("{Description} must not exceeds 500 characters");

            RuleFor(p => p.TaskStatus)
              .NotEmpty().WithMessage("{TaskStatus} is required")
              .NotNull();
        }
    }
}
