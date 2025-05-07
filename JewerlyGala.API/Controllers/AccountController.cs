using JewerlyGala.Application.Common.Security;
using JewerlyGala.Application.Features.Accounts.Queries.GetAllAccounts;
using JewerlyGala.Application.Features.Customers.DTOs;
using JewerlyGala.Application.Features.Customers.Queries.GetAllCustomer;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AccountController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllAccounts()
        {
            var customers = await Mediator.Send(new GetAllAccountsQuery());

            return Ok(customers);
        }
    }
}
