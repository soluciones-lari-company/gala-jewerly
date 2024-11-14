using AutoMapper;
using JewerlyGala.Application.Dtos;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.ItemModels.Queries.GetAllModels
{
    public class GetAllModelsQuery: IRequest<IEnumerable<ItemModelDto>>
    {
    }

    public class GetAllModelsQueryHandler : IRequestHandler<GetAllModelsQuery, IEnumerable<ItemModelDto>>
    {
        private IItemModelRepository itemModelRepository;
        private ILogger<GetAllModelsQueryHandler> logger;
        private IMapper mapper;
        public GetAllModelsQueryHandler(
            IItemModelRepository itemModelRepository,
            ILogger<GetAllModelsQueryHandler> logger,
            IMapper mapper)
        {
            this.itemModelRepository = itemModelRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ItemModelDto>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("getting all item models");

            var models = await itemModelRepository.GetAllItemModelsAsync();

            var modelsDto = mapper.Map<IEnumerable<ItemModelDto>>(models);

            return modelsDto;
        }
    }
}
