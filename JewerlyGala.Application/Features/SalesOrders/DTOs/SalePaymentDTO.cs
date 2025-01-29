using AutoMapper;
using JewerlyGala.Application.Features.Accounts.DTOs;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.SalesOrders.DTOs
{
    public class SalePaymentDTO: IMapFrom<SalePayment>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        //public Guid IdCustomer { get; set; }
        //public Guid IdAccount { get; set; }
        //public Guid? IdSaleOrder { get; set; }
        public decimal Total { get; set; }
        /// <summary>
        /// 01 - Efectivo
        /// 03 - transferencia de fondos
        /// 99 - por definir
        /// </summary>
        public string PaymentMethod { get; set; } = default!;
        public AccountDTO Account { get; set; }
        //public virtual Customer Customer { get; set; } = new();
        //public virtual Account Account { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalePayment, SalePaymentDTO>()
                .ForMember(d => d.Account, opt => opt.MapFrom(e => e.Account));
        }
    }
}
