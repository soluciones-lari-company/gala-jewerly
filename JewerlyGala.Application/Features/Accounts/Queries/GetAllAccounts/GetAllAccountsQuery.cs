using AutoMapper;
using JewerlyGala.Application.Features.Accounts.Commands.CreateAccount;
using JewerlyGala.Application.Features.Accounts.DTOs;
using JewerlyGala.Domain.Repositories.Accouting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Accounts.Queries.GetAllAccounts
{
    public class GetAllAccountsQuery : IRequest<IEnumerable<AccountDTO>>
    {
    }

    public class GetAllAccountsQueryHandler(
        ILogger<CreateAccountCommandHandler> logger,
        IAccountRepository accountRepository,
        IMapper mapper
        ) : IRequestHandler<GetAllAccountsQuery, IEnumerable<AccountDTO>>
    {
        public async Task<IEnumerable<AccountDTO>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetAllAccountsQuery");
            var accounts = await accountRepository.GetAllAsync();

            return mapper.Map<IEnumerable<AccountDTO>>(accounts);
        }
    }
}