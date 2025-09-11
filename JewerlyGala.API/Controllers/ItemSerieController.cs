using JewerlyGala.Application.Common.Security;
using JewerlyGala.Application.Features.ItemSeries.Command.AddFeatureToSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.CreateItemSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.RemoveFeatureToSerie;
using JewerlyGala.Application.Features.ItemSeries.Command.UpdateItemSerie;
using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetAllItemSeries;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetFeaturesValues;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetItemSerieById;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetItemSerieBySerieCode;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetSeriesBySerieCode;
using JewerlyGala.Domain.Constans;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyGala.API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ItemSerieController : ApiControllerBase
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Guid>> CreateItemSerie(CreateItemSerieCommand command)
        {
            var idItemSerieCreated = await Mediator.Send(command);

            return Ok(idItemSerieCreated);
        }

        [HttpPost("get-features-values")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureValuesDTO))]
        public async Task<ActionResult<FeatureValuesDTO>> GetFeatureValues()
        {
            var model = await Mediator.Send(new GetFeaturesValuesQuery());
            return Ok(model);
        }
        [HttpPost("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemSerieDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ItemSerieDTO>>> GetAll([FromBody] GetAllItemSeriesQuery command)
        {
            var model = await Mediator.Send(command);
            return Ok(model);
        }

        [HttpPost("get-alll-by-seriecode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemSeriePublicDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ItemSeriePublicDTO>>> GetSeriesBySerieCode([FromBody] GetSeriesBySerieCodeQuery command)
        {
            var model = await Mediator.Send(command);
            return Ok(model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ItemSerieDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ItemSerieDTO>> GetById(Guid id)
        {
            var model = await Mediator.Send(new GetItemSerieByIdQuery() { Id = id });
            return Ok(model);
        }

        [HttpGet("serial-code/{serieCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemSeriePublicDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ItemSeriePublicDTO>> GetBySerieCode(string serieCode)
        {
            var model = await Mediator.Send(new GetItemSerieBySerieCodeQuery() { serieCode = serieCode });
            return Ok(model);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> Update(Guid id, UpdateItemSerieCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpPost("{id}/features")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
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
        //[Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}/features")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> RemoveFeatureToSerie(Guid id, RemoveFeatureToSerieCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
