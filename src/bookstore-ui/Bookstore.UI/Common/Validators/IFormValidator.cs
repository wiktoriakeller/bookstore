using FluentValidation;

namespace Bookstore.UI.Common.Validators
{
    public interface IFormValidator<T> : IValidator<T>
    {
        Func<object, string, Task<IEnumerable<string>>> ValidateForm
        {
            get => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<T>
                    .CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));

                if (result.IsValid)
                {
                    return Enumerable.Empty<string>();
                }

                return result.Errors.Select(x => x.ErrorMessage);
            };
        }
    }
}
