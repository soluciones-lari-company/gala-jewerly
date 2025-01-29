using JewerlyGala.Application.Users.Commands.AssignUserRole;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpPost("user-role")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateModel(AssignUserRoleCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
