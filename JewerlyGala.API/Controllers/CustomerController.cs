using JewerlyGala.Application.Features.Customers.Commands.CreateCustomer;
using JewerlyGala.Application.Features.Customers.Commands.UpdateCustomer;
using JewerlyGala.Application.Features.Customers.DTOs;
using JewerlyGala.Application.Features.Customers.Queries;
using JewerlyGala.Application.Features.Customers.Queries.GetAllCustomer;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    public class CustomerController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateCustomerCommand command)
        {
            var customerIdCreated = await Mediator.Send(command);

            return Ok(customerIdCreated);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
        {
            var customers = await Mediator.Send(new GetAllCustomerQuery());

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById(Guid id)
        {
            var customer = await Mediator.Send(new GetCustomerByIdQuery() { CustomerId = id });
            return Ok(customer);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateCustomerCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
