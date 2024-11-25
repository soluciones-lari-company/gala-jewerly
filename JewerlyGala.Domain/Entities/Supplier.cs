using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class Supplier: BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; } = default!;
        public virtual ICollection<ItemSerie> ItemSeriesNav { get; set; } = [];
    }
}
