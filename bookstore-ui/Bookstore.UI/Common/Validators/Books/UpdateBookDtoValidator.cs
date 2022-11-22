using Bookstore.Core.Dtos.Books;
using FluentValidation;

namespace Bookstore.UI.Common.Validators
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>, IFormValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200);

            RuleFor(x => x.Genre)
                .NotNull()
                .NotEmpty()
                .WithMessage("Genre is required")
                .MaximumLength(200);

            RuleFor(x => x.Author)
                .NotNull()
                .NotEmpty()
                .WithMessage("Author is required")
                .MaximumLength(200);
        }
    }
}
