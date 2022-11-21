using Bookstore.Core.Dtos.Publishers;
using FluentValidation;

namespace Bookstore.BusinessLogic.Common.Validators.Publishers
{
    public class UpdatePublisherValidator : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
