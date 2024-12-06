using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateOnly? LastSale { get; set; }
        public DateOnly? LastPayment { get; set; }
        public int? Discount { get; set; }
        public virtual ICollection<SalesOrder> SalesOrdersNavigation { get; set; } = [];
        public virtual ICollection<SalePayment> Payments { get; set; } = [];
    }
}
