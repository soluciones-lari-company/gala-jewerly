using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class SalePayment: BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid IdCustomer { get; set; }
        public Guid IdAccount { get; set; }
        public Guid? IdSaleOrder { get; set; }
        public decimal Total { get; set; }
        /// <summary>
        /// 01 - Efectivo
        /// 03 - transferencia de fondos
        /// 99 - por definir
        /// </summary>
        public string PaymentMethod { get; set; } = default!;
        public virtual Customer Customer { get; set; } = new();
        public virtual Account Account { get; set; } = new();
        public virtual SalesOrder? SalesOrder { get; set; }
    }
}
