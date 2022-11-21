using Bookstore.Core.Dtos.Books;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Books
{
    public class BookFilterDtoValidator : AbstractValidator<BooksFiltersDto>
    {
        public BookFilterDtoValidator()
        {
            RuleFor(x => x.ReceptionFilters)
                .IsInEnum();
        }
    }
}
