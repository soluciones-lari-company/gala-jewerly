using JewerlyGala.Application.Features.Suppliers.Commands.CreateSupplier;
using JewerlyGala.Application.Features.Suppliers.DTOs;
using JewerlyGala.Application.Features.Suppliers.Queries.GetAllSuppliers;
using JewerlyGala.Application.Features.Suppliers.Queries.GetSupplierById;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class SupplierController : ApiControllerBase
    {
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSupplier(CreateSupplierCommand command)
        {
            var supplierIdCreated = await Mediator.Send(command);

            return Ok(supplierIdCreated);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAllSuppliers()
        {
            var suppliers = await Mediator.Send(new GetAllSuppliersQuery());

            return Ok(suppliers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetSupplierById(Guid id)
        {
            var supplier = await Mediator.Send(new GetSupplierByIdQuery() { Id = id });
            return Ok(supplier);
        }
    }
}
