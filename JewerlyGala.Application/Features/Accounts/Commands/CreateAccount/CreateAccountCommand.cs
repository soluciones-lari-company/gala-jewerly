using JewerlyGala.Domain.Repositories.Accouting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
    }

    public class CreateAccountCommandHandler(
        ILogger<CreateAccountCommandHandler> logger,
        IAccountRepository accountRepository
        ) : IRequestHandler<CreateAccountCommand, Guid>
    {
        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running CreateAccountCommandHandler");
            accountRepository.Account = new Domain.Entities.Account();
            accountRepository.Account.Name = request.Name;
            accountRepository.Account.Comments = request.Comments;
            accountRepository.Account.IsActive = false;

            await accountRepository.CreateAsync();

            return accountRepository.Account.Id;
        }
    }
}
