using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class SalePaymentOrder : BaseAuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Total { get; set; }
        public Guid IdSaleOrder { get; set; }
        public Guid IdSalePayment { get; set; }
        public Guid IdAccount { get; set; }

        public virtual SalesOrder? SaleOrder { get; set; }
        public virtual SalePayment? PaymentHeader { get; set; }
        public virtual Account? Account { get; set; }
    }
}
