using Bookstore.Core.Common;
using Bookstore.Core.Dtos.Books;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Books
{
    public class BookFilterDtoValidator : AbstractValidator<BooksFiltersDto>
    {
        public BookFilterDtoValidator()
        {
            RuleFor(x => x.ReceptionFilters)
                .Must((receptions, context) =>
                {
                    if (receptions.ReceptionFilters.Any(r => !Enum.IsDefined<Reception>(r)))
                    {
                        return false;
                    }

                    return true;
                })
                .WithMessage("Specified reception does not exist");
        }
    }
}
