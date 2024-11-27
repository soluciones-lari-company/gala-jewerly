using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories.Sales
{
    public class CustomerRepository(JewerlyDbContext dbContext) : ICustomerRepository
    {
        public async Task<Guid> CreateAsync(Customer customer)
        {
            if(customer == null) throw new ArgumentNullException(nameof(customer));

            await dbContext.Customers.AddAsync(customer);

            await dbContext.SaveChangesAsync();

            return customer.Id;
        }

        public async Task<bool> ExistsAsync(Guid idCustomer)
        {
            return await dbContext.Customers.AnyAsync(e => e.Id == idCustomer);
        }

        public async Task<ICollection<Customer>> GetAllAsync()
        {
            var customers = await dbContext.Customers.OrderBy(e => e.Name).ToListAsync();

            return customers;
        }

        public async Task<Customer?> GetByIdAsync(Guid idCustomer)
        {
            var customer = await dbContext.Customers.FirstOrDefaultAsync(e => e.Id == idCustomer);
            return customer;
        }

        public async Task UpdateAsync(Guid idCustomer, Customer customer)
        {
            var customerData = await dbContext.Customers.FirstOrDefaultAsync(e => e.Id == idCustomer);

            if(customerData != null)
            {
                customerData.Name = customer.Name;
                customerData.PhoneNumber = customer.PhoneNumber;
                customerData.Email = customer.Email;
                customerData.Discount = customer.Discount;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
