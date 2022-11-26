using Bookstore.Core.Dtos.Publishers;
using FluentValidation;

namespace Bookstore.UI.Common.Validators.Publishers
{
    public class AddPublisherDtoValidator : AbstractValidator<AddPublisherDto>, IFormValidator<AddPublisherDto>
    {
        public AddPublisherDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(200);
        }
    }
}
