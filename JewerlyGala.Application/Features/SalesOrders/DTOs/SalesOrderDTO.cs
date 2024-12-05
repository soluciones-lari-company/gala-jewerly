using AutoMapper;
using JewerlyGala.Application.Features.Customers.DTOs;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.SalesOrders.DTOs
{
    public class SalesOrderDTO : IMapFrom<SalesOrder>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateOnly Date { get; set; }
        public DateOnly? DueDate { get; set; }
        public string PaymentTerms { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public string PaymentConditions { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentaje { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Total { get; set; }
        public string Zone { get; set; } = default!;
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public virtual CustomerDTO? Customer { get; set; }
        public virtual ICollection<SaleOrderLineDTO> Lines { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrder, SalesOrderDTO>()
                .ForMember(d => d.Customer, opt => opt.MapFrom(e => e.CustomerNavigation))
                .ForMember(d => d.Lines, opt => opt.MapFrom(e => e.SaleOrderLinesNavigation));
        }
    }
}
