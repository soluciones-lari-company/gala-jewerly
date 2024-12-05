using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.SalesOrders.DTOs
{
    public class SaleOrderLineDTO : IMapFrom<SaleOrderLine>
    {
        public int Id { get; set; }
        public int NumLine { get; set; }
        public Guid ItemSerieId { get; set; }
        public string SerieCode { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentaje { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Total { get; set; }
        public decimal UnitPriceFinal { get; set; }
    }
}
