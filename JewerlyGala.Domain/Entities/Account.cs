using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class Account: BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public virtual ICollection<SalePaymentOrder> InCommingPayments { get; set; } = [];
    }
}
