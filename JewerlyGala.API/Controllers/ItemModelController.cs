using MediatR;
using JewerlyGala.Application.ItemModels.Commands.CreateItemModel;
using Microsoft.AspNetCore.Mvc;
using JewerlyGala.Application.ItemModels.Queries.GetAllModels;
using JewerlyGala.Application.ItemModels.Queries.GetModelById;

namespace JewerlyGala.API.Controllers
{
    //[ApiController]
    //[Route("api/item-model")]
    public class ItemModelController : ApiControllerBase
    {
        private IMediator mediator;

        public ItemModelController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await mediator.Send(new GetAllModelsQuery());
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await mediator.Send(new GetModelByIdQuery() { IdModel = id });
            if(model == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(CreateItemModelCommand command)
        {
            var idNewItemModel = await Mediator.Send(command);

            return Ok(idNewItemModel);
        }
    }
}
