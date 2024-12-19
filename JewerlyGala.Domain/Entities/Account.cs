using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class Account: BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        /// <summary>
        /// 01 - Efectivo
        /// 03 - transferencia de fondos
        /// 99 - por definir
        /// </summary>
        public string PaymentMethodAcceptable { get; set; } = default!;
        public virtual ICollection<SalePayment> InCommingPayments { get; set; } = [];
    }
}
