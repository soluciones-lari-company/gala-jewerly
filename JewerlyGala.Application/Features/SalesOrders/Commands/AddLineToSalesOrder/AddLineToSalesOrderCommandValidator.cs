using FluentValidation;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder
{
    public class AddLineToSalesOrderCommandValidator : AbstractValidator<AddLineToSalesOrderCommand>
    {
        public AddLineToSalesOrderCommandValidator()
        {
            RuleFor(v => v.SalesOrderId)
            .NotEmpty().WithMessage("Orden de venta no seleccionada");

            RuleFor(v => v.SerieCode)
            .NotEmpty().WithMessage("Codigo de serie vacio");

            RuleFor(v => v.Quantity)
            .NotEmpty().GreaterThan(0).WithMessage("Cantidad invalida");
        }
    }
}
