using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories.Sales
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(Customer customer);
        Task<bool> ExistsAsync(Guid idCustomer);
        Task<ICollection<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid idCustomer);
        Task UpdateAsync(Guid idCustomer, Customer customer);
    }
}
