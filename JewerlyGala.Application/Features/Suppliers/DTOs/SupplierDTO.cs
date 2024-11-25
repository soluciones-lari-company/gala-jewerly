using AutoMapper;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.Suppliers.DTOs
{
    public class SupplierDTO : IMapFrom<Supplier>
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; } = default!;
    }
}
