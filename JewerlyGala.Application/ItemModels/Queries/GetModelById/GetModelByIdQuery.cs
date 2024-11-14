using AutoMapper;
using JewerlyGala.Application.Dtos;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.ItemModels.Queries.GetModelById
{
    public class GetModelByIdQuery : IRequest<ItemModelDto>
    {
        public int IdModel { get;  set; }
    }

    public class GetModelByIdQueryHandler : IRequestHandler<GetModelByIdQuery, ItemModelDto>
    {
        private IItemModelRepository itemModelRepository;
        private ILogger<GetModelByIdQueryHandler> logger;
        private IMapper mapper;

        public GetModelByIdQueryHandler(
            IItemModelRepository itemModelRepository,
            ILogger<GetModelByIdQueryHandler> logger,
            IMapper mapper)
        {
            this.itemModelRepository = itemModelRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ItemModelDto> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"gettin model {request.IdModel}");

            var model = await itemModelRepository.GetByIdAsync(request.IdModel);

            var modelDto = mapper.Map<ItemModelDto>(model);

            return modelDto;
        }
    }
}
