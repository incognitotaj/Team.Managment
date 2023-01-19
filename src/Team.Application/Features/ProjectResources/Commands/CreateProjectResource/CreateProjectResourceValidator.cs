using FluentValidation;

namespace Team.Application.Features.ProjectResources.Commands.CreateProjectResource
{
    public class CreateProjectResourceValidator : AbstractValidator<CreateProjectResourceCommand>
    {
        public CreateProjectResourceValidator()
        {
            RuleFor(p => p.FromDate)
               .NotEmpty().WithMessage("{FromDate} is required")
               .NotNull();

            RuleFor(p => p.ToDate)
               .NotEmpty().WithMessage("{ToDate} is required")
               .NotNull();
        }
    }
}
