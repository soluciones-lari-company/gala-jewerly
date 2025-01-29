using JewerlyGala.Domain.Entities;


namespace JewerlyGala.Domain.Repositories.Accouting
{
    public interface ISalePaymentRepository
    {
        SalePayment SalePayment { get; set; }
        public Task<bool> GetAsync(Guid id);
        Task<Guid> CreateAsync();
        Task AddRange(ICollection<SalePayment> payments);
        Task<bool> UpdateAsync();
        void Remove(SalePayment payment);
        void RemoveRange(ICollection<SalePayment> payments);
        Task<ICollection<SalePayment>> GetAllAsync();
    }
}
