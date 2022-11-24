using Bookstore.Core.Dtos.Books;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Books
{
    public class BookFilterDtoValidator : AbstractValidator<BooksFiltersDto>
    {
        public BookFilterDtoValidator()
        {
            RuleFor(x => x)
                .Must((entity, context) =>
                {
                    if(entity.PublishDateStart > entity.PublishDateEnd)
                    {
                        return false;
                    }

                    return true;
                })
                .WithMessage("Start date should be before end date");
        }
    }
}
