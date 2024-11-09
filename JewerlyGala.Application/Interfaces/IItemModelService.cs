using JewerlyGala.Application.Dtos;

namespace JewerlyGala.Application.ItemModels
{
    public interface IItemModelService
    {
        Task<IEnumerable<ItemModelDto>> GetAllItemModelsAsync();
        Task<ItemModelDto> GetByIdAsync(int id);
    }
}