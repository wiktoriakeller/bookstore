using Bookstore.Core.Dtos.Publishers;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Publishers
{
    public class UpdatePublisherDtoValidator : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
