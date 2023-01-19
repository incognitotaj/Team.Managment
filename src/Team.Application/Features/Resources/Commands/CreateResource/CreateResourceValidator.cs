using FluentValidation;

namespace Team.Application.Features.Resources.Commands.CreateResource
{
    public class CreateResourceValidator : AbstractValidator<CreateResourceCommand>
    {
        public CreateResourceValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{FirstName} is required")
                .NotNull()
                .MaximumLength(150).WithMessage("{FirstName} must not exceeds 150 characters");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{LastName} is required")
                .NotNull()
                .MaximumLength(25).WithMessage("{LastName} must not exceeds 25 characters");

            RuleFor(p => p.Email)
                .MaximumLength(100).WithMessage("{Email} must not exceeds 100 characters");

            RuleFor(p => p.Phone)
                .MaximumLength(200).WithMessage("{Phone} must not exceeds 100 characters");
        }
    }
}
