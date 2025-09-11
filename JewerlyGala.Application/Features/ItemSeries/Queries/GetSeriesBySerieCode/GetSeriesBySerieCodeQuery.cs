using AutoMapper;
using JewerlyGala.Application.Common.Interfaces;
using JewerlyGala.Application.Features.ItemSeries.Command.UpdateItemSerie;
using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemSeries.Queries.GetSeriesBySerieCode
{
    public class GetSeriesBySerieCodeQuery: IRequest<IEnumerable<ItemSeriePublicDTO>>
    {
        public string serieCode { get; set; } = "";
    }

    public class GetSeriesBySerieCodeQueryHandler(
        ILogger<UpdateItemSerieCommandHandler> logger,
        IMapper mapper,
         IJewerlyDbContext dbContext
        ) : IRequestHandler<GetSeriesBySerieCodeQuery, IEnumerable<ItemSeriePublicDTO>>
    {
        public async Task<IEnumerable<ItemSeriePublicDTO>> Handle(GetSeriesBySerieCodeQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetSeriesBySerieCodeQuery");

            var series = await dbContext.ItemSeries
                .Include(e => e.ItemMaterialNav)
                .Where(e => e.SerieCode.Trim()
                .ToLower() == request.serieCode.Trim().ToLower()).OrderBy(e => e.Description).ToListAsync();

            if (series == null || series.Count == 0)
            {
                throw new NotFoundException($"Item serie not found {request.serieCode}");
            }

            var serieMAp = mapper.Map<IEnumerable<ItemSeriePublicDTO>>(series);
            return serieMAp;
        }
    }
}
