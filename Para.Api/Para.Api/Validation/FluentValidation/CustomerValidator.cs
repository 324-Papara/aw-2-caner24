using FluentValidation;
using Para.Data.Domain;

namespace Para.Api.Validation.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {

        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage($"{nameof(Customer.FirstName)} alani boş birakilamaz !.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage($"{nameof(Customer.LastName)} alani boş birakilamaz !.");
            RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().WithMessage($"{nameof(Customer.IdentityNumber)} alani boş birakilamaz !.");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage($"{nameof(Customer.Email)} alani boş birakilamaz !.");
            RuleFor(x => x.CustomerNumber).NotNull().NotEmpty().WithMessage($"{nameof(Customer.CustomerNumber)} alani boş birakilamaz !.");
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage($"{nameof(Customer.DateOfBirth)} alani boş birakilamaz !.");

        }
    }
}
