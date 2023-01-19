using FluentValidation;

namespace Team.Application.Features.ProjectResources.Commands.UpdateProjectResource
{
    public class UpdateProjectResourceValidator : AbstractValidator<UpdateProjectResourceCommand>
    {
        public UpdateProjectResourceValidator()
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
