using JewerlyGala.Application.ItemModels;
using JewerlyGala.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    [ApiController]
    [Route("api/item-model")]
    public class ItemModelController : ControllerBase
    {
        private IItemModelService itemModelService;

        public ItemModelController(IItemModelService itemModelService)
        {
            this.itemModelService = itemModelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await this.itemModelService.GetAllItemModelsAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await this.itemModelService.GetByIdAsync(id);
            if(model == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(model);
            }
        }
    }
}
