using AutoMapper;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemMaterials.Commands.CreateItemMaterial
{
    public class CreateItemMaterialCommand: IRequest<int>
    {
        public string MaterialName { get; set; } = default!;
    }

    public class CreateItemMaterialCommandHandler(
        ILogger<CreateItemMaterialCommandHandler> logger,
        IMapper mapper,
        IItemMaterialRepository itemMaterialRepository
        ) : IRequestHandler<CreateItemMaterialCommand, int>
    {
        public async Task<int> Handle(CreateItemMaterialCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("creating new item material");

            var material = await itemMaterialRepository.CreateAsync(request.MaterialName);

            return material;
        }
    }
}
