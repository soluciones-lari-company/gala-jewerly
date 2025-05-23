using JewerlyGala.Application.Common.Models;
using JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.AddPaymentInfoToSO;
using JewerlyGala.Application.Features.SalesOrders.Commands.AddPaymentToSO;
using JewerlyGala.Application.Features.SalesOrders.Commands.CancelSalesOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.ConfirmSaleOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.CreateSalesOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.DeleteLineFromOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.DeletePaymentToSO;
using JewerlyGala.Application.Features.SalesOrders.Commands.SaleOrderStep3Payment;
using JewerlyGala.Application.Features.SalesOrders.Commands.SetDiscountToSaleOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.SetWorkshopCostToOrder;
using JewerlyGala.Application.Features.SalesOrders.DTOs;
using JewerlyGala.Application.Features.SalesOrders.Queries.GetSalesOrderById;
using JewerlyGala.Application.Features.SalesOrders.Queries.GetSalesOrdersOpen;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JewerlyGala.API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SalesOrderController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult<Guid>> Create(CreateSalesOrderCommand command)
        {
            var salesOrderIdCreated = await Mediator.Send(command);

            return Ok(salesOrderIdCreated);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesOrderDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult<SalesOrderDTO>> GetSalesOrderById(Guid id)
        {
            var supplier = await Mediator.Send(new GetSalesOrderByIdQuery() { IdSalesOrder = id });
            return Ok(supplier);
        }

        [HttpGet("active-orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<SalesOrderDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult<ICollection<SalesOrderDTO>>> GetSalesOrdersOpen()
        {
            var supplier = await Mediator.Send(new GetSalesOrdersOpenQuery());
            return Ok(supplier);
        }

        [HttpPost("{id}/payment/payment-terms")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> AddPaymentInfo(Guid id, AddPaymentInfoToSOCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/payment/add-payment")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> AddPaymentToSO(Guid id, AddPaymentToSOCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}/payment/delete-payment")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> DeletePaymentToSO(Guid id, DeletePaymentToSOCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/confirm")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> ConfirmSaleOrder(Guid id, ConfirmSaleOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> CancelSalesOrder(Guid id, CancelSalesOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id}/set-discount")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> SetDiscountToSaleOrder(Guid id, SetDiscountToSaleOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id}/set-workshopCost")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> SetWorkshopCostToOrder(Guid id, SetWorkshopCostToOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/line")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult<int>> AddLineToSalesOrder(Guid id, AddLineToSalesOrderCommand command)
        {
            var lineIdCreated = await Mediator.Send(command);

            return Ok(lineIdCreated);
        }

        [HttpDelete("{id}/line/{idLine}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        public async Task<ActionResult> DeleteLineFromOrder(Guid id, Guid idLine, DeleteLineFromOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
