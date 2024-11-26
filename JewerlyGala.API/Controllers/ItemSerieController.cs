using JewerlyGala.Application.Features.ItemSeries.Command.AddFeatureToSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.CreateItemSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.RemoveFeatureToSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.UpdateItemSerie;
using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetAllItemSeries;
using JewerlyGala.Application.ItemSeries.Queries;
using JewerlyGala.Domain.Constans;
using JewerlyGala.Domain.Entities;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemSerieDTO>>> GetAll([FromBody] GetAllItemSeriesQuery command)
        {
            var model = await Mediator.Send(command);
            return Ok(model);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemSerie>> GetById(Guid id)
        {
            var model = await Mediator.Send(new GetItemSerieByIdQuery() { Id = id });
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ItemSerie>> Update(Guid id, UpdateItemSerieCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id}/features")]
        public async Task<ActionResult<Guid>> AddFeatureToSerie(Guid id, AddFeatureToSerieCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
        [HttpDelete("{id}/features")]
        public async Task<ActionResult<Guid>> RemoveFeatureToSerie(Guid id, RemoveFeatureToSerieCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
