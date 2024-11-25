using AutoMapper;
using JewerlyGala.Application.Features.ItemMaterials.DTOs;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
namespace JewerlyGala.Application.Features.ItemMaterials.Queries.GetItemMaterialById
{
    public class GetItemMaterialByIdQuery: IRequest<ItemMaterialDTO>
    {
        public int IdMaterial { get; set; }
    }

    public class GetItemMaterialByIdQueryHandler(
        ILogger<GetItemMaterialByIdQueryHandler> logger, 
        IMapper mapper, 
        IItemMaterialRepository itemMaterialRepository
        ) : IRequestHandler<GetItemMaterialByIdQuery, ItemMaterialDTO>
    {
        public async Task<ItemMaterialDTO> Handle(GetItemMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("getting item material by id");

            var material = await itemMaterialRepository.GetById(request.IdMaterial);

            if (material == null)
            {
                throw new NotFoundException("item material not found");
            }

            return mapper.Map<ItemMaterialDTO>( material ); 
        }
    }
}
