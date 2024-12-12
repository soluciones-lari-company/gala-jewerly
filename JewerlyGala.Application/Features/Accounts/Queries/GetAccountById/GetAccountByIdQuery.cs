using AutoMapper;
using JewerlyGala.Application.Features.Accounts.Commands.CreateAccount;
using JewerlyGala.Application.Features.Accounts.DTOs;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Accouting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Accounts.Queries.GetAccountById
{
    public class GetAccountByIdQuery : IRequest<AccountDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetAccountByIdQueryHandler(
        ILogger<CreateAccountCommandHandler> logger,
        IAccountRepository accountRepository,
        IMapper mapper
        ) : IRequestHandler<GetAccountByIdQuery, AccountDTO>
    {
        public async Task<AccountDTO> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetAllAccountsQuery");
            
            var existsAccount = await accountRepository.GetAsync(request.Id);

            if(!existsAccount)
            {
                throw new NotFoundException("account not found");
            }

            return mapper.Map<AccountDTO>(accountRepository.Account);
        }
    }
}
