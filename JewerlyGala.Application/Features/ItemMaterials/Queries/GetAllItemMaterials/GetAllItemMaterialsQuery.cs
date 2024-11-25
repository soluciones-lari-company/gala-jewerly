using AutoMapper;
using JewerlyGala.Application.Features.ItemMaterials.DTOs;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemMaterials.Queries.GetAllItemMaterials
{
    public class GetAllItemMaterialsQuery : IRequest<IEnumerable<ItemMaterialDTO>>
    {
    }

    public class GetAllItemMaterialsQueryHandler(
        ILogger<GetAllItemMaterialsQueryHandler> logger,
        IMapper mapper,
        IItemMaterialRepository itemMaterialRepository
        ) : IRequestHandler<GetAllItemMaterialsQuery, IEnumerable<ItemMaterialDTO>>
    {
        public async Task<IEnumerable<ItemMaterialDTO>> Handle(GetAllItemMaterialsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("getting all item materials");

            var materials = await itemMaterialRepository.GetAllAsync();

            return mapper.Map<IEnumerable<ItemMaterialDTO>>(materials);
        }
    }
}
