using AutoMapper;
using JewerlyGala.Application.Features.SalesOrders.DTOs;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.Customers.DTOs
{
    public class CustomerProfileDTO : IMapFrom<Customer>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateOnly? LastSale { get; set; }
        public DateOnly? LastPayment { get; set; }
        public int? Discount { get; set; }
        public virtual ICollection<SalesOrderDTO> Orders { get; set; } = [];
        public virtual ICollection<SalePaymentDTO> Payments { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerProfileDTO>()
                .ForMember(d => d.Orders, opt => opt.MapFrom(e => e.SalesOrdersNavigation));
        }

    }
}
