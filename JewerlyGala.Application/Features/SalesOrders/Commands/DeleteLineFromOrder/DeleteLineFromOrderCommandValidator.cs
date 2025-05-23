using FluentValidation;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.DeleteLineFromOrder
{
    public class DeleteLineFromOrderCommandValidator: AbstractValidator<DeleteLineFromOrderCommand>
    {
        public DeleteLineFromOrderCommandValidator()
        {
            RuleFor(v => v.SalesOrderId)
            .NotEmpty().WithMessage("Orden de venta no seleccionada");

            RuleFor(v => v.ItemSerieId)
            .NotEmpty().WithMessage("Codigo de serie vacio");

            RuleFor(v => v.Quantity)
            .NotEmpty().GreaterThan(0).WithMessage("Cantidad invalida");
        }
    }
}
