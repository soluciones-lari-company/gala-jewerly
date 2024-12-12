using JewerlyGala.Application.Features.Accounts.Commands.CreateAccount;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Accouting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommand: IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class UpdateAccountCommandHandler(
        ILogger<UpdateAccountCommandHandler> logger,
        IAccountRepository accountRepository
        ) : IRequestHandler<UpdateAccountCommand>
    {
        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running CreateAccountCommandHandler");

            var exists = await accountRepository.GetAsync( request.Id );

            if(!exists)
            {
                throw new NotFoundException("account not found");
            }

            accountRepository.Account.Name = request.Name;
            accountRepository.Account.Comments = request.Comments;
            accountRepository.Account.IsActive = false;

            await accountRepository.UpdateAsync();

        }
    }
}
