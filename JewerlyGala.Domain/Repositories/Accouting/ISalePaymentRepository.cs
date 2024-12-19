using JewerlyGala.Domain.Entities;


namespace JewerlyGala.Domain.Repositories.Accouting
{
    public interface ISalePaymentRepository
    {
        SalePayment Account { get; set; }
        public Task<bool> GetAsync(Guid id);
        Task<Guid> CreateAsync();
        Task<bool> UpdateAsync();
        Task<ICollection<Account>> GetAllAsync();

    }
}
