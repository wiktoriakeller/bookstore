using Bookstore.Core.Dtos.Publishers;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Publishers
{
    public class AddPublisherValidator : AbstractValidator<AddPublisherDto>
    {
        public AddPublisherValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
