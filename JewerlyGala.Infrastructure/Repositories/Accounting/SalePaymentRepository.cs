using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories.Accouting;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories.Accounting
{
    public class SalePaymentRepository(
        JewerlyDbContext dbContext
        ) : ISalePaymentRepository
    {
        public SalePayment SalePayment { get; set; } = new ();

        public async Task AddRange(ICollection<SalePayment> payments)
        {
             await dbContext.SalePayments.AddRangeAsync(payments);
        }

        public async Task<Guid> CreateAsync()
        {
            await dbContext.SalePayments.AddAsync(SalePayment);

            return SalePayment.Id;
        }

        public async Task<ICollection<SalePayment>> GetAllAsync()
        {
            return await dbContext.SalePayments.OrderBy(e => e.Date).ToListAsync();
        }

        public async Task<bool> GetAsync(Guid id)
        {
            var payment_ = await dbContext.SalePayments
                .FirstOrDefaultAsync(e => e.Id == id);


            if (payment_ == null)
            {
                return false;
            }
            else
            {
                SalePayment = payment_;
                return true;
            }
        }

        public void Remove(SalePayment payment)
        {
            dbContext.SalePayments.Remove(payment);
        }

        public void RemoveRange(ICollection<SalePayment> payments)
        {
            dbContext.SalePayments.RemoveRange(payments);
        }

        public async Task<bool> UpdateAsync()
        {
            if (SalePayment.Id == Guid.Empty)
            {
                throw new InvalidOperationException(nameof(SalePayment.Id));
            }

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
