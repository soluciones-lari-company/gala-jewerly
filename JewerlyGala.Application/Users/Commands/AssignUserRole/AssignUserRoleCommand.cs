using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using JewerlyGala.Domain.Identity;
using JewerlyGala.Domain.Exceptions;

namespace JewerlyGala.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommand: IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }

    public class AssignUserRoleCommandHandler(
        ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
        ) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("assigning user role {@request}", request);
            
            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if(user == null)
            {
                throw new NotFoundException($"User {request.UserEmail} not found");
            }

            var role = await roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException($"Role {request.RoleName} not found");

            await userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}
