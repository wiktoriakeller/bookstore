using Bookstore.Core.Dtos.Publishers;
using FluentValidation;

namespace Bookstore.UI.Common.Validators.Publishers
{
    public class UpdatePublisherDtoValidator : AbstractValidator<UpdatePublisherDto>, IFormValidator<UpdatePublisherDto>
    {
        public UpdatePublisherDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(200);
        }
    }
}
