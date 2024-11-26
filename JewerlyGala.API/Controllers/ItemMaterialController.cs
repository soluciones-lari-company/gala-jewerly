using JewerlyGala.Application.Features.ItemMaterials.Commands.CreateItemMaterial;
using JewerlyGala.Application.Features.ItemMaterials.DTOs;
using JewerlyGala.Application.Features.ItemMaterials.Queries.GetAllItemMaterials;
using JewerlyGala.Application.Features.ItemMaterials.Queries.GetItemMaterialById;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    public class ItemMaterialController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateItemMaterial(CreateItemMaterialCommand command)
        {
            var materialIdCreated = await Mediator.Send(command);

            return Ok(materialIdCreated);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemMaterialDTO>>> GetAllItemMaterial()
        {
            var materials = await Mediator.Send(new GetAllItemMaterialsQuery());

            return Ok(materials);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemMaterialDTO>> GetAllItemMaterial(int id)
        {
            var material = await Mediator.Send(new GetItemMaterialByIdQuery() { IdMaterial  = id });
            return Ok(material);
        }
    }
}
