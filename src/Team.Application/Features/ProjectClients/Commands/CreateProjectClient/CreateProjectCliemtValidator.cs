using FluentValidation;
using Team.Application.Features.ProjectClients.Commands.CreateProjectClient;

namespace Team.Application.Features.ProjectClients.Commands.CreateProject
{
    public class CreateProjectCliemtValidator : AbstractValidator<CreateProjectClientCommand>
    {
        public CreateProjectCliemtValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Name} must not exceeds 100 characters");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{Phone} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Email} must not exceeds 100 characters");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("{Phone} is required")
                .NotNull()
                .MaximumLength(30).WithMessage("{Phone} must not exceeds 30 characters");
        }
    }
}
