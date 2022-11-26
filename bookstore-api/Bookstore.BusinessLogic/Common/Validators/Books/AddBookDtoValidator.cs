using Bookstore.Core.Dtos.Books;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Books
{
    public class AddBookDtoValidator : AbstractValidator<AddBookDto>
    {
        public AddBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Genre)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Reception)
                .IsInEnum()
                .WithMessage("Specified reception does not exist");
        }
    }
}
