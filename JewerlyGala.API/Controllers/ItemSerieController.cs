using JewerlyGala.Application.Features.ItemSeries.Command.AddFeatureToSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.CreateItemSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.RemoveFeatureToSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.UpdateItemSerie;
using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetAllItemSeries;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetItemSerieById;

//using JewerlyGala.Application.ItemSeries.Queries;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    public class ItemSerieController : ApiControllerBase
    {
        [HttpPost("create")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateItemSerie(CreateItemSerieCommand command)
        {
            var idItemSerieCreated = await Mediator.Send(command);

            return Ok(idItemSerieCreated);
        }

        [HttpPost("get-all")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemSerieDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ItemSerieDTO>>> GetAll([FromBody] GetAllItemSeriesQuery command)
        {
            var model = await Mediator.Send(command);
            return Ok(model);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ItemSerieDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ItemSerieDTO>> GetById(Guid id)
        {
            var model = await Mediator.Send(new GetItemSerieByIdQuery() { Id = id });
            return Ok(model);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update(Guid id, UpdateItemSerieCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpPost("{id}/features")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> AddFeatureToSerie(Guid id, AddFeatureToSerieCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
        /// <summary>
        /// asdadasdasdasdas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}/features")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> RemoveFeatureToSerie(Guid id, RemoveFeatureToSerieCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
