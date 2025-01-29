using JewerlyGala.Application.Features.Customers.Commands.CreateCustomer;
using JewerlyGala.Application.Features.Customers.Commands.UpdateCustomer;
using JewerlyGala.Application.Features.Customers.DTOs;
using JewerlyGala.Application.Features.Customers.Queries;
using JewerlyGala.Application.Features.Customers.Queries.GetAllCustomer;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class CustomerController : ApiControllerBase
    {
        [Authorize(Roles = UserRoles.Admin)]
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateCustomerCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
