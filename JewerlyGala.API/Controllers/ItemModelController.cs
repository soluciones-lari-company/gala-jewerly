using MediatR;
using JewerlyGala.Application.ItemModels.Commands.CreateItemModel;
using Microsoft.AspNetCore.Mvc;
using JewerlyGala.Application.ItemModels.Queries.GetAllModels;
using JewerlyGala.Application.ItemModels.Queries.GetModelById;
using JewerlyGala.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using JewerlyGala.Domain.Constans;

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
        public async Task<ActionResult<IEnumerable<ItemModelDto>>> GetAll()
        {
            var models = await mediator.Send(new GetAllModelsQuery());
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModelDto>> GetById(int id)
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
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateModel(CreateItemModelCommand command)
        {
            var idNewItemModel = await Mediator.Send(command);

            return Ok(idNewItemModel);
        }
    }
}
