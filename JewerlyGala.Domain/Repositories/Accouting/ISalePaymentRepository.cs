using JewerlyGala.Domain.Entities;


namespace JewerlyGala.Domain.Repositories.Accouting
{
    public interface ISalePaymentRepository
    {
        public Task<Guid> CreateAsync(SalePayment payment);

    }
}
