using JewerlyGala.Application.Dtos;
using JewerlyGala.Application.ItemModels.Queries.GetModelById;
using JewerlyGala.Application.ItemSeries.Commands;
using JewerlyGala.Application.ItemSeries.Queries;
using JewerlyGala.Domain.Constans;
using JewerlyGala.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    public class ItemSerieController : ApiControllerBase
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Guid>> CreateItemSerie(CreateItemSerieCommand command)
        {
            var idItemSerieCreated = await Mediator.Send(command);

            return Ok(idItemSerieCreated);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemSerie>> GetById(Guid id)
        {
            var model = await Mediator.Send(new GetItemSerieByIdQuery() { Id = id });
            return Ok(model);
        }
    }
}
