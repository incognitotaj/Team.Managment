using FluentValidation;

namespace Team.Application.Features.ProjectDocuments.Commands.CreateProjectDocument
{
    public class CreateProjectDocumentValidator : AbstractValidator<CreateProjectDocumentCommand>
    {
        public CreateProjectDocumentValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{Name} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Name} must not exceeds 100 characters");

            RuleFor(p => p.Document)
                .NotEmpty().WithMessage("{Document} is required")
                .NotNull();

            RuleFor(p => p.Detail)
                .NotEmpty().WithMessage("{Detail} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Detail} must not exceeds 500 characters");
        }
    }
}
