using FluentValidation;

namespace JewerlyGala.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator() 
        { 
            RuleFor(c => c.Name)
                .NotEmpty();

            RuleFor(c => c.PhoneNumber)
                .NotEmpty();
        }
    }
}
