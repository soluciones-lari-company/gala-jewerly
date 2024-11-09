using AutoMapper;
using JewerlyGala.Application.Dtos;
using JewerlyGala.Application.ItemModels;
using JewerlyGala.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Services.ItemModels
{
    public class ItemModelService : IItemModelService
    {
        private IItemModelsRepository itemModelsRepository;
        private ILogger<ItemModelService> logger;
        private IMapper mapper;

        public ItemModelService(
            IItemModelsRepository itemModelsRepository, 
            ILogger<ItemModelService> logger,
            IMapper mapper)
        {
            this.itemModelsRepository = itemModelsRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ItemModelDto>> GetAllItemModelsAsync()
        {
            logger.LogInformation("getting all item models");

            var models = await itemModelsRepository.GetAllItemModelsAsync();

            var modelsDto = mapper.Map<IEnumerable<ItemModelDto>>(models);

            return modelsDto;
        }

        public async Task<ItemModelDto> GetByIdAsync(int id)
        {
            logger.LogInformation($"gettin model {id}");

            var model = await itemModelsRepository.GetByIdAsync(id);

            var modelDto = mapper.Map<ItemModelDto>(model);

            return modelDto;
        }
    }
}
