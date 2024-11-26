using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories.Sales
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(Guid idCustomer);
        Task UpdateAsync(Customer customer);
    }
}
