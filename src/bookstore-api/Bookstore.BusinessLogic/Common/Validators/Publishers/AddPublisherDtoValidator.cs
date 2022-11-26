using Bookstore.Core.Dtos.Publishers;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Publishers
{
    public class AddPublisherDtoValidator : AbstractValidator<AddPublisherDto>
    {
        public AddPublisherDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
