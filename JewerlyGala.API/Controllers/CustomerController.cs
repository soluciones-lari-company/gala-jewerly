using JewerlyGala.Application.Common.Models;
using JewerlyGala.Application.Common.Security;
using JewerlyGala.Application.Features.Customers.Commands.CreateCustomer;
using JewerlyGala.Application.Features.Customers.Commands.UpdateCustomer;
using JewerlyGala.Application.Features.Customers.DTOs;
using JewerlyGala.Application.Features.Customers.Queries;
using JewerlyGala.Application.Features.Customers.Queries.GetAllCustomer;
using JewerlyGala.Application.Features.Customers.Queries.GetCustomerPortafolio;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CustomerController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Guid>> Create(CreateCustomerCommand command)
        {
            var customerIdCreated = await Mediator.Send(command);

            return Ok(customerIdCreated);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
        {
            var customers = await Mediator.Send(new GetAllCustomerQuery());

            return Ok(customers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CustomerDTO>> GetById(Guid id)
        {
            var customer = await Mediator.Send(new GetCustomerByIdQuery() { CustomerId = id });
            return Ok(customer);
        }

        [HttpGet("portafolio/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CustomerDTO>> GetCustomerPortafolio(Guid id)
        {
            var customer = await Mediator.Send(new GetCustomerPortafolioQuery() { CustomerId = id });
            return Ok(customer);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(Guid id, UpdateCustomerCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
