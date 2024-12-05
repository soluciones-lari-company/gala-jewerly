using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories.Sales
{
    public interface ISalesOrderRepository
    {
        SalesOrder Order
        {
            get;
            set;
        }
        Task<bool> GetByIdAsync(Guid idsalesOrder);
        Task<Guid> CreateAsync();
        Task<bool> UpdateAsync();
        Task<ICollection<SalesOrder>> GetAllAsync();

        //Task<Guid> CreateAsync(SalesOrder order);
        //Task<SalesOrder?> GetByIdAsync(Guid idsalesOrder);
        //Task<ICollection<SalesOrder>> GetAllAsync();
        //Task UpdateAsync(SalesOrder order);
        //Task<int> AddLineAsync(Guid idsalesOrder, SaleOrderLine line);
        //Task UpdateLineAsync(Guid idsalesOrder, SaleOrderLine line);
        //Task RemoveLineAsync(Guid idsalesOrder, SaleOrderLine line);
    }
}
