using JewerlyGala.Domain.Common;

namespace JewerlyGala.Domain.Entities
{
    public class SalesOrder : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid IdCustomer { get; set; }
        public DateOnly Date { get; set; }
        public DateOnly? DueDate { get; set; }
        /// <summary>
        /// PUE - pago en una sola exhibición
        /// PPD - Pago en Parcialidades o Diferido
        /// </summary>
        public string PaymentTerms { get; set; } = default!;
        /// <summary>
        /// 01 - Efectivo
        /// 03 - transferencia de fondos
        /// 99 - por definir
        /// </summary>
        public string PaymentMethod { get; set; } = default!;
        public string PaymentConditions { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentaje { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Total { get; set; }
        public string Zone { get; set; } = default!;
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public virtual Customer? CustomerNavigation { get; set; }
        public virtual ICollection<SaleOrderLine> SaleOrderLinesNavigation { get; set; } = [];
    }
}
