using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.Customers.DTOs
{
    public class CustomerDTO : IMapFrom<Customer>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateOnly? LastSale { get; set; }
        public DateOnly? LastPayment { get; set; }
        public int? Discount { get; set; }
    }
}
