using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class ItemSerie: BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string SerieCode { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public int QuantitySold { get; set; }
        public int QuantityCommited { get; set; }
        public int QuantityFree { get; set; }
        public Guid SupplierId { get; set; }
        public string PurchaseUnitMeasure { get; set; } = default!;
        public decimal PurchasePriceByUnitMeasure { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public decimal PurchaseUnitPrice { get; set; }
        public int SalePercentRentability { get; set; }
        public decimal SaleUnitPrice { get; set; }

        public virtual Supplier? SupplierNav { get; set; }
        public virtual ItemMaterial? ItemMaterialNav { get; set; }
    }
}
