using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories.Accouting;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories.Accounting
{
    public class AccountRepository(
        JewerlyDbContext dbContext
        ) : IAccountRepository
    {
        public Account Account { get; set; } = new Account();

        public async Task<Guid> CreateAsync()
        {
            await dbContext.Accounts.AddAsync( Account );   

            return Account.Id;
        }

        public async Task<ICollection<Account>> GetAllAsync()
        {
            return await dbContext.Accounts.OrderBy(e => e.Name).ToListAsync();
        }

        public async Task<bool> GetAsync(Guid id)
        {
            var account_ = await dbContext.Accounts
                .FirstOrDefaultAsync(e => e.Id == id);


            if (account_ == null)
            {
                return false;
            }
            else
            {
                Account = account_;
                return true;
            }
        }

        public async  Task<bool> UpdateAsync()
        {
            if (Account.Id == Guid.Empty)
            {
                throw new InvalidOperationException(nameof(Account.Id));
            }

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
