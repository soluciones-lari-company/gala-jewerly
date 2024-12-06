using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class SalePayment: BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid IdCustomer { get; set; }
        public decimal Total { get; set; }
        public decimal TotalApplied { get; set; }
        public decimal TotalFree { get; set; }
        /// <summary>
        /// 01 - Efectivo
        /// 03 - transferencia de fondos
        /// 99 - por definir
        /// </summary>
        public string PaymentMethod { get; set; } = default!;
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<SalePaymentOrder> SalePaymentsApplied { get; set; } = [];
    }
}
