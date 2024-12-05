using JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.CancelSalesOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.ConfirmSaleOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.CreateSalesOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.DeleteLineFromOrder;
using JewerlyGala.Application.Features.SalesOrders.Commands.SaleOrderStep3Payment;
using JewerlyGala.Application.Features.SalesOrders.DTOs;
using JewerlyGala.Application.Features.SalesOrders.Queries.GetSalesOrderById;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JewerlyGala.API.Controllers
{
    public class SalesOrderController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateSalesOrderCommand command)
        {
            var salesOrderIdCreated = await Mediator.Send(command);

            return Ok(salesOrderIdCreated);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrderDTO>> GetSalesOrderById(Guid id)
        {
            var supplier = await Mediator.Send(new GetSalesOrderByIdQuery() { IdSalesOrder = id });
            return Ok(supplier);
        }

        [HttpPost("{id}/step3Payment")]
        public async Task<ActionResult<SalesOrderDTO>> SaleOrderStep3Payment(Guid id, SaleOrderStep3PaymentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/confirm")]
        public async Task<ActionResult<SalesOrderDTO>> ConfirmSaleOrder(Guid id, ConfirmSaleOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id}/cancel")]
        public async Task<ActionResult<SalesOrderDTO>> CancelSalesOrder(Guid id, CancelSalesOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/line")]
        public async Task<ActionResult<int>> AddLineToSalesOrder(Guid id, AddLineToSalesOrderCommand command)
        {
            var lineIdCreated = await Mediator.Send(command);

            return Ok(lineIdCreated);
        }

        [HttpDelete("{id}/line/{idLine}")]
        public async Task<ActionResult> DeleteLineFromOrder(Guid id, Guid idLine, DeleteLineFromOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
